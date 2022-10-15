using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.ViewModels
{
    public class StudentListing
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; } 
        
        public int Gender { get; set; } 

        public string ClassName { get; set; }

    }
}
