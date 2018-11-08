using Microsoft.EntityFrameworkCore;
using Students.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Data
{
    public class StudentAdminContext : DbContext
    {
        public StudentAdminContext(DbContextOptions<StudentAdminContext> options) : base(options) { }

        public DbSet<Student> Student { get; set; }
        public DbSet<Mark> Mark { get; set; }
        public DbSet<Subject> Subject { get; set; }
    }
}
