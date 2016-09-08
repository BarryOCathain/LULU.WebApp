using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace LULU.WebApp.Models
{
    public class ClassViewModel
    {
        public int ClassID { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        [DisplayName("Class Date")]
        [DisplayFormat(DataFormatString ="dd/mm/yyyy", ApplyFormatInEditMode = true)]
        public DateTime ClassDate { get; set; }

        [Required]
        public bool Compulsory { get; set; }

        [Required]
        [DisplayName("Start Time")]
        public TimeSpan StartTime { get; set; }

        [Required]
        [DisplayName("End Time")]
        public TimeSpan EndTime { get; set; }

        [Required]
        public int CourseID { get; set; }
    }
}