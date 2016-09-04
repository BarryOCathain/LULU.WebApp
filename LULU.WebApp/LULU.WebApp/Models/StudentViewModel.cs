using LULU.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LULU.WebApp.Models
{
    public class StudentViewModel
    {
        public StudentViewModel()
        {
            Courses = new List<Course>();
            AttendedClasses = new List<AtttendedClass>();
        }

        public int UserID { get; set; }

        [Required]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        public int Password { get; set; }

        [Required]
        [DisplayName("Student Number")]
        public string StudentNumber { get; set; }

        public List<Course> Courses { get; set; }

        public List<AtttendedClass> AttendedClasses { get; set; }
    }
}