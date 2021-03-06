using Examination.Data.Models;
using Examination.Data.Services;
using Examination.WEB.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.WEB.Controllers
{


    [Authorize(Roles = "Administrator")]
    public class AdminTestController : Controller
    {
        private readonly ILogger<AdminTestController> _logger;
        private readonly IExaminationData _examinationDataProvider;

        public AdminTestController(ILogger<AdminTestController> logger, IExaminationData examinationDataProvider)
        {
            _logger = logger;
            _examinationDataProvider = examinationDataProvider;
        }

        public async Task<IActionResult> Index(int? id, int? questionId)
        {
            var model = new TestsIndexViewModel();
            model.Tests = await _examinationDataProvider.GetTests();

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
        public async Task<IActionResult> Create(Test test)
        {
            if (ModelState.IsValid)
            {
                await _examinationDataProvider.AddTest(test);
                return RedirectToAction("Index", "AdminTest");
            }
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var model = await _examinationDataProvider.GetTest(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int id, IFormCollection formCol)
        {
            await _examinationDataProvider.DeleteTest(id);
            return RedirectToAction("Index", "AdminTest");
        }

        
        [HttpGet]
        public async Task<IActionResult> Edit(int id) 
        {
            var model = await _examinationDataProvider.GetTest(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(Test test, IFormCollection form)
        {
            if (test.ErrorThreshold < 0)
            {
                ModelState.AddModelError(nameof(test.ErrorThreshold), "Error threshold shold be not negative");
            }

            if (ModelState.IsValid)
            {
                await _examinationDataProvider.UpdateTest(test);
                return RedirectToAction("Index", "AdminTest");
            }
            
            return View(test);
        }
    }
}
