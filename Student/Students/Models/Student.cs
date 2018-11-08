using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace Students.Models
{
    public class Student
    {
        [Key] public string RollNumber { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public DateTime CreateAt { get; set; }
        public DateTime UpdateAt { get; set; }
        public StudentStatus Status { get; set; }
        public StudentGender Gender { get; set; }
        public List<Mark> Marks { get; set; }

    }

    public enum StudentGender
    {
        Male = 1,
        Female = 0,
        Other = 2
    }

    public enum StudentStatus
    {
        Active = 1,
        DeActive = 0
    }
}
