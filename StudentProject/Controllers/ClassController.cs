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
    public class ClassController : Controller
    {
        private ClassesService _classServise;
        private StudentsService _studentsService;
     
        public ClassController(ClassesService classesServise , StudentsService studentsService)
        {
            _classServise = classesServise;
            _studentsService = studentsService; 
        }
        public IActionResult Index()
        {

            return View();

        }
        [HttpPost]
        public async Task<IActionResult> LoadClasses(DataTablesParam<ClassListing> model)
        {

            model = await _classServise.SearchClasses(model);
            return Json(model);
        }

        [HttpPost]
        public async Task<JsonResult> AddClass(ClassVM newObj)
        {
            string message;
            bool isItemExist = await _classServise.checkIfClassExist(newObj);
            if (isItemExist)
            {
                message = "exist";
                return Json( message );

            }
            else
            {
                var result = await _classServise.AddClass(newObj);
                if (result)
                {
                    message = "success";
                }
                else
                {
                    message = "error";
                }

                return Json(message);

            }
        }


        public async Task<IActionResult> deleteClass(int id)
        {
            try
            {
               bool isClassContainStudents=  _studentsService.CheckIfClassContainStudents(id);
                if (isClassContainStudents)
                {
                    return Json("Related");
                }
                else
                {
                    bool isRecordDeleted = await _classServise.DeleteClass(id);
                    if (isRecordDeleted)
                    {
                        return Json("Success");
                    }
                    else
                    {
                        return Json("Failed");
                    }
                }
            }
            catch (Exception ex)
            {
                return Json("Failed");
            }


        }
        }

    }
