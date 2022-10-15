using AutoMapper;
using StudentProject.Models;
using StudentProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.AutoMapperProfiles
{
    public class StudentsStudentListingProfile:Profile
    {
        public StudentsStudentListingProfile()
        {
            CreateMap<Student, StudentListing>().ForMember(x => x.ClassName, opt => { opt.MapFrom(c => c.Class.Name); });
        }
    }
}
