using AutoMapper;
using StudentProject.Models;
using StudentProject.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.AutoMapperProfiles
{
    public class ClassListingClassProfile:Profile
    {
        public ClassListingClassProfile()
        {
            CreateMap<Class, ClassListing>();
         }
    }
}
