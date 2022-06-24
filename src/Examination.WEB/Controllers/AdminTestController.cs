using Examination.Data.Models;
using Examination.Data.Services;
using Examination.WEB.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.WEB.Controllers
{
    public class AdminTestController : Controller
    {
        private readonly ILogger<AdminTestController> _logger;
        private readonly IExaminationData _db;

        public AdminTestController(ILogger<AdminTestController> logger, IExaminationData examinationData)
        {
            _logger = logger;
            _db = examinationData;
        }

        public IActionResult Index(int? id, int? questionId)
        {
            var model = new TestsIndexViewModel();
            model.Tests = _db.GetTests();

            if (id != null) 
            {
                ViewBag.TestId = id.Value;
                ViewBag.Test = model.Tests.Where(t => t.Id == id.Value).SingleOrDefault();
                model.Questions = ((Test)ViewBag.Test).Questions;
            }

            if (questionId != null) 
            {
                ViewBag.QuestionId = questionId;
                ViewBag.Question = ((Test)ViewBag.Test).Questions.Where(q => q.Id == questionId).SingleOrDefault();
                model.Answers = ((Question)ViewBag.Question).Answers;
            }

            return View(model);
        }
        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [ValidateAntiForgeryToken]
        [HttpPost]
        public IActionResult Create(Test test)
        {
            if (ModelState.IsValid)
            {
                _db.AddTest(test);
                return RedirectToAction("Index", "AdminTest");
            }
            return View();
        }

        [HttpGet]
        public IActionResult Delete(int id)
        {
            var model = _db.GetTest(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Delete(int id, IFormCollection formCol)
        {
            _db.DeleteTest(id);
            return RedirectToAction("Index", "AdminTest");
        }

        [HttpGet]
        public IActionResult Edit(int id) 
        {
            var model = _db.GetTest(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Edit(Test test, IFormCollection form)
        {
            _db.UpdateTest(test);
            
            return RedirectToAction("Index", "AdminTest");
        }
    }
}
