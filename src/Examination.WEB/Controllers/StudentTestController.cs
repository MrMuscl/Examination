using Examination.Data.Models;
using Examination.Data.Services;
using Examination.WEB.Utils;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.WEB.Controllers
{
    public class StudentTestController : Controller
    {
        private readonly ILogger<StudentTestController> _logger;
        private readonly IExaminationData _db;

        public StudentTestController(ILogger<StudentTestController> logger)
        {
            _logger = logger;
            _db = new ExaminationData(new ExaminationContext());
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _db.GetTests();
            return View(model);
        }

        [HttpGet]
        public IActionResult Execute(int id, int? questionNumber) 
        {
            Question question;
            
            var test = _db.GetTestWithQuestionsAndAnswers(id);
            ViewBag.QuestionNumber = questionNumber?? 1;
            ViewBag.QuestionCount = test.Questions.Count();
            ViewBag.TestId = id;

            if (questionNumber == null)
            {
                question = test.Questions.FirstOrDefault();                
            }
            else
            {
                question = test.Questions.Where(q => q.Number == questionNumber).FirstOrDefault();
            }

            //if (question == null)
            //    return NotFoundResult

            return View(question);
        }

        [HttpPost]
        public IActionResult Execute(string navigationBtn, IFormCollection form) 
        {
            int questionNumber;
            Helper.GetFormIntValue(form, "QuestionNumber", out questionNumber);

            int testId;
            Helper.GetFormIntValue(form, "TestId", out testId);

            switch (navigationBtn) 
            {
                case "Prev":
                    return RedirectToAction("Execute", "StudentTest", new { Id = testId, QuestionNumber = questionNumber - 1 });
                case "Next":
                    return RedirectToAction("Execute", "StudentTest", new { Id = testId, QuestionNumber = questionNumber + 1 });
            }
            return RedirectToAction("Execute", "StudentTest", new { Id = testId, QuestinNumber = 1 });
        }
    }
}
