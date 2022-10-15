using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Models
{
    public class Student:BaseEntity
    {
              [Required]
         public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required]
        public int Gender { get; set; }
        [Required]
        public string ImagePath { get; set; }
        [Required]
        public string Address { get; set; }
        [Required]
        public string EmailAddress { get; set; }
        [Required]
        public string PhoneNumber { get; set; }

        [DataType(DataType.Date)]
        public DateTime DateOfBirth { get; set; }


        [ForeignKey("ClassId")]
        public Class Class { get; set; }








    }
}
