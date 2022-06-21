using Examination.Data.Models;
using Examination.Data.Services;
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
            ViewBag.QuestionNumber = questionNumber;

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
        public IActionResult ExecutePrev(int id, int? questionNumber)
        {
            var question = _db.GetQuestion(id);
            ViewBag.QuestionNumber = questionNumber;

            
            
            return RedirectToAction("Execute", "StudentTest", new { Id = 1, QuestinNumber = questionNumber - 1});
        }

        [HttpPost]
        public IActionResult ExecuteNext(int id, int? questionNumber)
        {
            var question = _db.GetQuestion(id);
            ViewBag.QuestionNumber = questionNumber;

            return RedirectToAction("Execute", "StudentTest", new { Id = 1, QuestinNumber = questionNumber + 1 });
        }
    }
}
