using LULU.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LULU.WebApp.Models
{
    public class CourseViewModel
    {
        public CourseViewModel()
        {
            Classes = new List<Class>();
        }

        public int CourseID { get; set; }

        [Required]
        [DisplayName("Course Code")]
        public string CourseCode { get; set; }

        [Required]
        public string Name { get; set; }

        public List<Class> Classes { get; set; }
    }
}