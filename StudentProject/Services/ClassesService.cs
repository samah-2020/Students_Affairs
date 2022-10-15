using AutoMapper;
using Microsoft.EntityFrameworkCore;
using StudentProject.Models;
using StudentProject.ViewModels;
using StudentProject.ViewModels.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Services
{
    public class ClassesService
    {
        private IAsyncRepository<Class> _classesRepositry;
        private IMapper _mapper;
        public ClassesService(IAsyncRepository<Class> classRepositry, IMapper mapper)
        {
            _classesRepositry = classRepositry;
            _mapper = mapper;
        }

   
        public async Task<bool> AddClass(ClassVM newObj)
        {
            try
            {
                Class NewEntityobj = new Class();
                NewEntityobj.Name = newObj.Name.ToLower();
                await _classesRepositry.AddAsync(NewEntityobj);
                await _classesRepositry.SaveChangesAsync();
                return true;

            }
            catch (Exception ex)
            {
                return false;

            }
        } 
       
        public async Task< bool> DeleteClass(int id)
        {
            try
            {
                var ClassObj = await _classesRepositry.GetByIdAsync(id);

                if (ClassObj != null)
                {

                    await _classesRepositry.DeleteAsync(ClassObj, true);

                    return true; 
                }
            }
            catch (Exception ex)
            {

                return false; 
            }

            return false; 

        }

        public async Task<bool> checkIfClassExist(ClassVM newObj)
        {
            try
            {
               var Items = _classesRepositry.GetAllAsQueryable().Where(c=>c.Name.ToLower()==newObj.Name.ToLower());
                if (Items != null && Items.Count()>0)
                    return true;
                else
                    return false;
                

            }
            catch (Exception ex)
            {
                return false;

            }
        }
            public async Task<DataTablesParam<ClassListing>> SearchClasses(DataTablesParam<ClassListing> model)
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

                var query = _classesRepositry.GetAllAsQueryable();

               

                model.recordsTotal = await query.CountAsync();
                if (!string.IsNullOrWhiteSpace(model.search.value))
                {
                    var searchText = model.search.value.ToLower();
                    query = query.Where(c => c.Name.ToLower().Contains(searchText) );
                }
                if (!string.IsNullOrWhiteSpace(orderColumn))
                {
                    switch (orderColumn)
                    {

                     
                        case "name":
                            if (orderDirection == OrderDirection.Ascending)
                                query = query.OrderBy(c => c.Name);
                            else
                                query = query.OrderByDescending(c => c.Name);
                            break;


                    }

                }
                model.recordsFiltered = await query.CountAsync();

                var Allclasses = await query.Skip(numberOfItemsToSkip).
                    Take(numberOfItemsToTake). ToListAsync();


                model.data = _mapper.Map<List<ClassListing>>(Allclasses);
                return model;
            

        }
        public  List<Class> GetAllClasses()
        {
            var allClasses =  _classesRepositry.GetAllAsQueryable().ToList();
            return allClasses; 

           
        }

    }
}
