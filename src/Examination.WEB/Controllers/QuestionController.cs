using Examination.Data.Models;
using Examination.Data.Services;
using Examination.WEB.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Primitives;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.WEB.Controllers
{
    public class QuestionController : Controller
    {
        private readonly ILogger<QuestionController> _logger;
        private readonly IExaminationData _db;

        public QuestionController(ILogger<QuestionController> logger, IExaminationData examinationData)
        {
            _logger = logger;
            _db = examinationData;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add(int id) 
        {
            var test = _db.GetTest(id);
            ViewBag.Test = test;
            return View();
        }
        [HttpPost]
        public IActionResult Add(Question question, IFormCollection form)
        {
            int testId = 0;
            Helper.GetFormIntValue(form, "TestId", out testId);
            
            _db.AddNewQuestionToTest(question, testId);

            return RedirectToAction("Index", "AdminTest", new { Id = testId });
        }

        [HttpGet]
        public IActionResult Remove(int id, int? testId) 
        {
            var model = _db.GetQuestion(id);
            if (testId != null) 
            {
                ViewBag.TestId = testId.Value;
            }

            ViewBag.QuestionId = id;
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Remove(Question question, IFormCollection form) 
        {
            int questionId;
            Helper.GetFormIntValue(form, "QuestionId", out questionId);

            int testId;
            Helper.GetFormIntValue(form, "TestId", out testId);
            
            _db.DeleteQuestion(questionId);

            return RedirectToAction("Index", "Admintest", new { id = testId});
        }

        [HttpGet]
        public IActionResult Edit(int id, int? testId)
        {
            var question = _db.GetQuestion(id);
            ViewBag.QuestionNumber = question.Number;

            if (testId != null)
            {
                ViewBag.TestId = testId.Value;
            }
            
            return View(question);
        }

        public IActionResult Edit(Question question, IFormCollection form)
        {
            int testId;
            Helper.GetFormIntValue(form, "TestId", out testId);

            int questionNumber;
            Helper.GetFormIntValue(form, "QuestionNumber", out questionNumber);
            
            question.Number = questionNumber;
            _db.UpdateQuestion(question);

            return RedirectToAction("Index", "AdminTest", new { id = testId });
        }
    }
}
