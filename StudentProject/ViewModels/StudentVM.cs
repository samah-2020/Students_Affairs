using Microsoft.AspNetCore.Http;
using StudentProject.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.ViewModels
{
    public class StudentVM
    {
        [Required (ErrorMessage ="enter first name")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "enter Last name")]
        public string LastName { get; set; }
        [Required(ErrorMessage = " Choose gender")]
        public int Gender { get; set; }
        [Required(ErrorMessage = "enter Image")]
        public IFormFile Image { get; set; }
        [Required(ErrorMessage = "enter address")]
        public string Address { get; set; }
        [Required(ErrorMessage = "enter Email")]
        [EmailAddress(ErrorMessage = "Invalid Email Address")]
        public string EmailAddress { get; set; }
        [Required(ErrorMessage = "enter phone number")]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid phone number")]
        public string PhoneNumber { get; set; } 
         [Required(ErrorMessage ="enter Date of Birth")] 
        [DisplayFormat(DataFormatString = "{0:dd/MM/yyyy}", ApplyFormatInEditMode = true)] 
         
        public DateTime DateOfBirth { get; set; }
        public string Class { get; set; }
        public List<Class> AllClasses { get; set; }
    }
}
