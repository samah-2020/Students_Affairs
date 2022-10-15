using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;

using System.Linq;
using System.Threading.Tasks;

namespace StudentProject.Models
{
    public class StudentsAffairsContext:DbContext
    {

        public DbSet<Student> Students { get; set; }
        public DbSet<Class> Classes { get; set; }
        public StudentsAffairsContext(DbContextOptions<StudentsAffairsContext> options) : base(options)
        {
        }



    }
}

