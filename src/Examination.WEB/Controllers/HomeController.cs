using Examination.Data.Models;
using Examination.Data.Services;
using Examination.WEB.Models;
using Microsoft.AspNetCore.Http;
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
        [HttpGet]
        public IActionResult CreateTest()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult CreateTest(Test test)
        {
            if (ModelState.IsValid)
            {
                _db.AddTest(test);
                return RedirectToAction("Admin");
            }
            return View();
        }
        
        [HttpGet]
        public IActionResult DeleteTest(int id) 
        {
            var model = _db.GetTest(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult DeleteTest(int id, IFormCollection formCol)
        {
            _db.DeleteTest(id);
            return RedirectToAction("Admin");
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
