using LULU.Model;
using LULU.Model.Common;
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

        [Required]
        public string Surname { get; set; }

        [Required]
        [EmailAddress]
        public string Email { get; set; }

        public string Password { get; set; }

        [Required]
        [DisplayName("Student Number")]
        public string StudentNumber { get; set; }

        [DisplayName("Password Reset")]
        public bool ResetPassword { get; set; }

        public List<Course> Courses { get; set; }

        public List<AtttendedClass> AttendedClasses { get; set; }
    }

    public class StudentClassesViewModel
    {
        private Service.StudentClient studentContext;
        private Service.ClassClient classContext;

        public List<Class> UpcomingClasses { get; set; }
        public List<Class> AttendedClasses { get; set; }
        public List<Class> MissedClasses { get; set; }

        public StudentClassesViewModel(int studentUserID)
        {
            studentContext = new Service.StudentClient();
            classContext = new Service.ClassClient();

            var studentNumber = GetStudentNumber(studentUserID);
            LoadUpcomingClasses(studentNumber);
            LoadAttendedClasses(studentNumber);
            LoadMissedClasses(studentNumber);
        }

        private string GetStudentNumber(int studentUserID)
        {
            var student = Serializers<Student>.Deserialize(studentContext.GetStudentByUserID(studentUserID));

            return student.StudentNumber;
        }

        private void LoadUpcomingClasses(string studentNumber)
        {
            var classes = Serializers<Class>.DeserializeList(classContext.GetClassesByStudentNumberAndDateRange(studentNumber, DateTime.Now.Date, DateTime.Now.AddDays(1).Date, false));

            if (classes != null)
                UpcomingClasses = classes;
            else
                UpcomingClasses = new List<Class>();
        }

        private void LoadAttendedClasses(string studentNumber)
        {
            var classes = Serializers<Class>.DeserializeList(classContext.GetAttendedClassesByStudentNumberAndDateRange(studentNumber, DateTime.Now.AddDays(-7).Date, DateTime.Now.Date));

            if (classes != null)
                AttendedClasses = classes;
            else
                AttendedClasses = new List<Class>();
        }

        private void LoadMissedClasses(string studentNumber)
        {
            var classes = Serializers<Class>.DeserializeList(classContext.GetMissedClassesByStudentNumberAndDateRange(studentNumber, DateTime.Now.AddDays(-7).Date, DateTime.Now.Date));

            if (classes != null)
                MissedClasses = classes;
            else
                MissedClasses = new List<Class>();
        }
    }
}