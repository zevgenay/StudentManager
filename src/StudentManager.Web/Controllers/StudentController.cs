using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using StudentManager.Core.Entities;
using StudentManager.Infrastructure.Data;
using StudentManager.Infrastructure.Repositories;
using StudentManager.Infrastructure.Repositories.Impl;
using StudentManager.Web.Models;

namespace StudentManager.Web.Controllers
{
    public class StudentController : Controller
    {
        private readonly IStudentRepository _studentRepository;

        public StudentController(AppDbContext appDbContext)
        {
            _studentRepository = new StudentRepository(appDbContext);
        }

        [HttpPost]
        public JsonResult Create(StudentForm studentForm)
        {
            Student student = new Student
            {
                FirstName = studentForm.FirstName,
                LastName = studentForm.LastName,
                MiddleName = studentForm.MiddleName,
                NickName = studentForm.NickName                
            };

            _studentRepository.Add(student);

            return Json(student);
        }
    }
}