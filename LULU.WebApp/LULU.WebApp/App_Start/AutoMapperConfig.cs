using AutoMapper;
using LULU.Model;
using LULU.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LULU.WebApp.App_Start
{
    public static class AutoMapperConfig
    {
        public static void RegisterMappings()
        {
            Mapper.Initialize(cfg =>
            {
                cfg.CreateMap<Course, CourseViewModel>();
                cfg.CreateMap<CourseViewModel, Course>();

                cfg.CreateMap<Student, StudentViewModel>();
                cfg.CreateMap<StudentViewModel, Student>();
            });
        }
    }
}