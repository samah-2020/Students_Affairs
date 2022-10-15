using AutoMapper;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using StudentProject.Models;
using StudentProject.ViewModels;
using StudentProject.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace StudentProject.Services
{
    public class StudentsService
    {
        private IAsyncRepository<Student> _studentRepositry;
        private IAsyncRepository<Class> _classRepositry;
        private readonly IHostingEnvironment _hostEnvironment;
        private IMapper _mapper;
        public StudentsService(IAsyncRepository<Student> studentRepositry, IHostingEnvironment HostEnvironment , IAsyncRepository<Class> classRepositry, IMapper Mapper)
        {
            _studentRepositry = studentRepositry;
            _hostEnvironment = HostEnvironment;
            _classRepositry = classRepositry; 
            _mapper = Mapper; 
        }

        public async Task DeleteStudent(Student student)
        {
            await _studentRepositry.DeleteAsync(student, true);
        } 
   

        public async Task<DataTablesParam<StudentListing>> SearchStudents(DataTablesParam<StudentListing> model)
        {
            int numberOfItemsToTake = model.length;
            int numberOfItemsToSkip = model.start;
            string orderColumn = "";
            OrderDirection orderDirection = OrderDirection.Ascending;
            if (model.order != null && model.order.Count > 0)
            {
                orderColumn = model.columns[int.Parse(model.order[0].column)].name;
                orderDirection = model.order[0].dir.Equals("asc", StringComparison.InvariantCultureIgnoreCase)
                    ? OrderDirection.Ascending
                    : OrderDirection.Descending;
            }

            var query = _studentRepositry.GetAllAsQueryable();



            model.recordsTotal = await query.CountAsync();
            if (!string.IsNullOrWhiteSpace(model.search.value))
            {
                var searchText = model.search.value.ToLower();
                query = query.Where(c => c.FirstName.ToLower().Contains(searchText) || c.LastName.ToLower().Contains(searchText));
            }

            if (model.columns[3].search.value != null && model.columns[3].search.value != "select")
            {
                var classSearchVal = int.Parse(model.columns[3].search.value);
                query = query.Where(c => c.Class.Id.Equals(classSearchVal));
            }
            if (!string.IsNullOrWhiteSpace(orderColumn))
            {
                switch (orderColumn)
                {


                    case "firstName":
                        if (orderDirection == OrderDirection.Ascending)
                            query = query.OrderBy(c => c.FirstName);
                        else
                            query = query.OrderByDescending(c => c.FirstName);
                        break;
                    case "lastName":
                        
                        if (orderDirection == OrderDirection.Ascending)
                            query = query.OrderBy(c => c.LastName);
                        else
                            query = query.OrderByDescending(c => c.LastName);
                        break;
                           
                    default:
                        query = query.OrderBy(c => c.FirstName);
                        break; 

                }

            }
            model.recordsFiltered = await query.CountAsync();

            var Allstudents = await query.Include(s=>s.Class).Skip(numberOfItemsToSkip).
                Take(numberOfItemsToTake).ToListAsync();


            model.data = _mapper.Map<List<StudentListing>>(Allstudents);
            return model;


        }
        public bool CheckIfClassContainStudents(int classId)
        {
          var studentsPerClass= _studentRepositry.GetAllAsQueryable().Include(c => c.Class).Where(s => s.Class.Id == classId);
            if (studentsPerClass != null && studentsPerClass.Count() > 0)
                return true;
            else
                return false; 

           
        }
        public async Task<bool> checkIfStudentExist(StudentVM newObj)
        {
            try
            {
                var Items = _studentRepositry.GetAllAsQueryable().Where(c => c.EmailAddress.ToLower() == newObj.EmailAddress.ToLower() || c.PhoneNumber==newObj.PhoneNumber);
                if (Items != null && Items.Count() > 0)
                    return true;
                else
                    return false;


            }
            catch (Exception ex)
            {
                return false;

            }
        }



        public async Task<bool> DeleteStudent(int id)
        {
            try
            {
                var studentObj = await _studentRepositry.GetByIdAsync(id);

                if (studentObj != null)
                {

                    await _studentRepositry.DeleteAsync(studentObj, true);

                    return true;
                }
            }
            catch (Exception ex)
            {

                return false;
            }

            return false;

        }

        public async Task<bool> AddStudent(StudentVM newStudent)
        {
            try
            {
                Class relatedClass = null ; 
                if (newStudent.Class != null)
                {
                     relatedClass = await _classRepositry.GetByIdAsync(Convert.ToInt32(newStudent.Class));
                }
                    string ImagePath = SaveImage(newStudent.Image); 
                    Student NewEntityobj = new Student();
                    NewEntityobj.FirstName = newStudent.FirstName;
                    NewEntityobj.LastName = newStudent.LastName;
                    NewEntityobj.Gender = newStudent.Gender;
                    NewEntityobj.ImagePath = ImagePath ;
                    NewEntityobj.PhoneNumber = newStudent.PhoneNumber;
                    NewEntityobj.EmailAddress = newStudent.EmailAddress;
                    NewEntityobj.Address = newStudent.Address;
                    NewEntityobj.DateOfBirth = newStudent.DateOfBirth; 
                if(relatedClass != null)
                {
                    NewEntityobj.Class = relatedClass; 

                }
                    


                    await _studentRepositry.AddAsync(NewEntityobj);
                    await _studentRepositry.SaveChangesAsync();
                    return true;
                }  
            
            catch (Exception ex)
            {
                return false;

            }

        }
        public string SaveImage(IFormFile image)
        {
            string imagesLocalPath = _hostEnvironment.WebRootPath + "\\Images";  //local
            string uniqueFileName = Guid.NewGuid().ToString("N") + System.IO.Path.GetExtension(image.FileName);

            using (var fileStream = new FileStream(imagesLocalPath + "\\" + uniqueFileName, FileMode.Create))
            {
                image.CopyTo(fileStream);
            }
            return imagesLocalPath + "/Images/" + uniqueFileName;

        }
    }
}
