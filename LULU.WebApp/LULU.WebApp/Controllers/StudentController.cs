using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LULU.WebApp.Controllers
{
    public class StudentController : Controller
    {
        private Service.StudentClient studentContext;

        public StudentController()
        {
            studentContext = new Service.StudentClient();
        }

        // GET: Student
        public ActionResult Index()
        {
            

            return View();
        }
    }
}