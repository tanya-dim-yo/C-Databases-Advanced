using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace P01_StudentSystem.Data.Models
{
    public class Course
    {
        public int CourseId { get; set; }

        [MaxLength(Constants.CourseNameMaxLength)]
        [Unicode]
        public string? Name { get; set; }

        [Unicode]
        public string? Description { get; set; }

        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal Price { get; set; }

        public virtual ICollection<StudentCourse>? StudentsCourses { get; set; }

        public virtual ICollection<Resource>? Resources { get; set; }

        public virtual ICollection<Homework>? Homeworks { get; set; }
    }
}