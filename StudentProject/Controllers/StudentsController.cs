using Microsoft.AspNetCore.Mvc;
using StudentProject.Models;
using StudentProject.Services;
using StudentProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Controllers
{
    public class StudentsController : Controller
    {
        private StudentsService _studentServise;
        private ClassesService _classServise;
        public StudentsController(StudentsService studentServise , ClassesService classServise)
        {
            _studentServise = studentServise;
            _classServise = classServise; 
        }
        public IActionResult Index()
        {
            var availableClasses= _classServise.GetAllClasses(); 
            return View(availableClasses);

        }
        [HttpPost]
        public async Task<IActionResult> LoadStudentsWithClass(DataTablesParam<StudentListing> model)
        {

          model = await _studentServise.SearchStudents(model);
            return Json(model);
        } 
        [HttpGet]
        public async Task<ActionResult> Create()
        {
            
            var studentViewModel = new StudentVM();
            studentViewModel.DateOfBirth = DateTime.Now;
            studentViewModel.AllClasses = _classServise.GetAllClasses(); 
            return View(studentViewModel); 
        }
        [HttpPost]
        public async Task<ActionResult> Create(StudentVM studentVM)
        {
            var studentViewModel = new StudentVM();
            studentViewModel.DateOfBirth = DateTime.Now;
            studentViewModel.AllClasses = _classServise.GetAllClasses();
            bool isItemExist = await _studentServise.checkIfStudentExist(studentVM);
         
                if (ModelState.IsValid)
                {
                    if (isItemExist)
                {
                    ViewBag.exist = true;

                    return View(studentViewModel);

                }
                else
                {
                    var isStudentAdded = await _studentServise.AddStudent(studentVM);
                    if (isStudentAdded)
                    {
                        return RedirectToAction("Index");
                    }
                  
                    return View(studentViewModel);
                }
               

            } 
                else
            {
                return View(studentViewModel);
            }

        }

        public async Task<IActionResult> deleteStudent(int id)
        {
            try
            {
                bool isRecordDeleted = await _studentServise.DeleteStudent(id);
                if (isRecordDeleted)
                {
                    return Json("Success");
                }
                else
                {
                    return Json("Failed");
                }
            }
            catch (Exception ex)
            {
                return Json("Failed");
            }


        }


    }
}
 