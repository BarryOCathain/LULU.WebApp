using AutoMapper;
using LULU.Model;
using LULU.Model.Common;
using LULU.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace LULU.WebApp.Controllers
{
    public class CourseController : Controller
    {
        private Service.CourseClient courseContext;
        private Service.ClassClient classContext;

        public CourseController()
        {
            courseContext = new Service.CourseClient();
            classContext = new Service.ClassClient();
        }

        [AcceptVerbs(HttpVerbs.Get|HttpVerbs.Post)]
        public ActionResult Index()
        {
            List<Course> courses = Serializers<Course>.DeserializeList(courseContext.GetAllCourses());

            return View(courses);
        }

        public ActionResult CourseDetails(int id)
        {
            var course = Serializers<Course>.Deserialize(courseContext.GetCourseByID(id));

            if (course != null)
                return View(course);
            return View();
        }

        public ActionResult View(int id)
        {
            var course = GetCourseDetails(id);

            if (course != null)
                return View(course);
            return View();
        }

        [HttpGet]
        public ActionResult Edit(int id)
        {
            var course = GetCourseDetails(id);

            if (course != null)
                return View(course);
            return View();
        }

        public ActionResult Delete(int id)
        {
            var result = courseContext.DeleteCourse(id);

            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(CourseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var course = Mapper.Map<Course>(viewModel);
                var courseStr = Serializers<Course>.Serialize(course);


                if (courseContext.UpdateCourse(Serializers<Course>.Serialize(course)))
                    return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult New()
        {
            return View();
        }

        [HttpPost]
        public ActionResult New(CourseViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var course = Mapper.Map<Course>(viewModel);

                courseContext.AddCourse(course.CourseCode, course.Name);

                return RedirectToAction("Index");
            }
            return View(viewModel);
        }

        [HttpGet]
        public ActionResult Class(int id)
        {
            return View(new ClassViewModel() { CourseID = id });
        }

        [HttpPost]
        public ActionResult Class(ClassViewModel viewModel)
        {
            if (ModelState.IsValid)
            {
                var _class = Mapper.Map<Class>(viewModel);
                var course = Serializers<Course>.Deserialize(courseContext.GetCourseByID(viewModel.CourseID));

                if (_class != null)
                {
                    _class.Course = course;
                    var result = classContext.AddClass(Serializers<Class>.Serialize(_class));

                    if (result)
                        return RedirectToAction("Course", new { id = viewModel.CourseID });
                }
                ModelState.AddModelError("", "An Error occurred saving the new Class.");
            }
            return View(viewModel);
        }

        private CourseViewModel GetCourseDetails(int courseID)
        {
            var course = Serializers<Course>.Deserialize(courseContext.GetCourseByID(courseID));

            if (course != null)
            {
                var viewModel = Mapper.Map<CourseViewModel>(course);
                var classes = Serializers<Class>.DeserializeList(classContext.GetAllClassesByCourse(courseID));

                if (classes != null)
                {
                    classes.OrderBy(c => c.ClassDate).OrderBy(c => c.StartTime);
                    viewModel.Classes = classes;
                }
                return viewModel;
            }
            return null;
        }
    }
}