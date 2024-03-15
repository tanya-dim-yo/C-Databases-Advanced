using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_StudentSystem.Data.Models
{
    public class Student
    {
        public int StudentId { get; set; }

        [MaxLength(Constants.StudentNameMaxLength)]
        [Unicode]
        public string? Name { get; set; }

        [StringLength(Constants.StudentPhoneNumberExactLength)]
        public string? PhoneNumber { get; set; }

        public DateTime RegisteredOn { get; set; }

        public DateTime? Birthday { get; set; }

        public virtual ICollection<StudentCourse>? StudentsCourses { get; set; }
        public virtual ICollection<Homework>? Homeworks { get; set; }
    }
}