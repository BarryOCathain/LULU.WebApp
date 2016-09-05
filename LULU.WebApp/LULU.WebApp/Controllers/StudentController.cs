using AutoMapper;
using LULU.Model;
using LULU.Model.Common;
using LULU.WebApp.Models;
using LULU.WebApp.Properties;
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
            var students = Serializers<Student>.DeserializeList(studentContext.GetAllStudents());

            return View(students);
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var student = Serializers<Student>.Deserialize(studentContext.GetStudentByUserID(id));

            if (student != null)
            {
                var svm = Mapper.Map<StudentViewModel>(student);
                return View(svm);
            }
            return View();
        }

        [HttpPost]
        public ActionResult Edit(StudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                if (viewModel.ResetPassword)
                    viewModel.Password = Settings.Default.DefaultPassword;

                var student = Mapper.Map<Student>(viewModel);

                if (student != null)
                {
                    var result = studentContext.UpdateStudent(student.StudentNumber, student.FirstName, student.Surname, student.Email, student.Password);

                    if (result)
                        return RedirectToAction("Index");

                    ModelState.AddModelError("", "An Error Occurred Updating the Student.");
                }
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View(new StudentViewModel());
        }

        [HttpPost]
        public ActionResult New(StudentViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                viewModel.Password = Settings.Default.DefaultPassword;

                var student = Mapper.Map<Student>(viewModel);

                if (student != null)
                {
                    var result = studentContext.CreateStudent(student.StudentNumber, student.FirstName, student.Surname, student.Email, student.Password);

                    if (result)
                        return RedirectToAction("Index");

                    ModelState.AddModelError("", "An Error occurred Creating the New Student.");
                }
            }
            return View(viewModel);
        }

        public ActionResult Delete(string studentNumber)
        {
            var result = studentContext.DeleteStudent(studentNumber);

            return RedirectToAction("Index");
        }
    }
}