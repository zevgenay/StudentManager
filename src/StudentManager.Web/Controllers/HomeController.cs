using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using StudentManager.Core.Entities;
using StudentManager.Infrastructure.Data;
using StudentManager.Web.Models;

namespace StudentManager.Web.Controllers
{
    public class HomeController : Controller
    {
        private AppDbContext _appDbContext;
        private readonly ILogger<HomeController> _logger;

        public HomeController(AppDbContext appDbContext, ILogger<HomeController> logger)
        {
            _appDbContext = appDbContext;
            _logger = logger;
        }

        public IActionResult Index()
        {
            //Student s1 = new Student { FirstName = "Tom", LastName = "Sawyer", Gender = Core.Entities.Enums.Gender.Male, NickName = "TSawyer" };

            //_appDbContext.Students.Add(s1);
            //_appDbContext.SaveChanges();

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
