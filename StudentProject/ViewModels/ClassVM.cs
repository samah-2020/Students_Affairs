using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.ViewModels
{
    public class ClassVM
    {   
        [Required(ErrorMessage ="Enter Class Name")]
        public string Name { get; set; }
    }
}
