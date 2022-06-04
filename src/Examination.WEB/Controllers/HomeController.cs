using Examination.Data.Models;
using Examination.Data.Services;
using Examination.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.WEB.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IExaminationData _db; 
                         
        public HomeController(ILogger<HomeController> logger)
        {
            _logger = logger;
            _db = new ExaminationData(new ExaminationContext());
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }
        [HttpGet]
        public IActionResult Tests()
        {
            var model = _db.GetTestsWithAnswers();
            return View(model);
        }

        [HttpGet]
        public IActionResult Admin() 
        {
            var model = _db.GetTestsWithAnswers();
            return View(model);
        }

        public IActionResult CreateTest() 
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
