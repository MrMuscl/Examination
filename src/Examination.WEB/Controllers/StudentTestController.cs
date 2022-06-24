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

        public StudentTestController(ILogger<StudentTestController> logger, IExaminationData examinationData)
        {
            _logger = logger;
            _db = examinationData;
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
            if (questionNumber == null) 
            {
                var attestation = new Attestation();
                attestation.StartTime = DateTime.Now;
                attestation.IsActive = true;
                _db.AddAttestation(attestation);
            }

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
            ViewBag.CheckedAnswer = question.Protocol?.AnswerId ?? 0;
            
            return View(question);
        }

        [HttpPost]
        public IActionResult Execute(string navigationBtn, IFormCollection form) 
        {
            int questionNumber;
            Helper.GetFormIntValue(form, "QuestionNumber", out questionNumber);

            int questionId;
            Helper.GetFormIntValue(form, "QuestionId", out questionId);

            int testId;
            Helper.GetFormIntValue(form, "TestId", out testId);

            int answerId;
            Helper.GetFormIntValue(form, "Answer", out answerId);

            switch (navigationBtn) 
            {
                case "Prev":
                    _db.AddProtocol(questionId, answerId);
                    return RedirectToAction("Execute", "StudentTest", new { Id = testId, QuestionNumber = questionNumber - 1 });
                case "Next":
                    _db.AddProtocol(questionId, answerId);
                    return RedirectToAction("Execute", "StudentTest", new { Id = testId, QuestionNumber = questionNumber + 1 });
                case "Complete":
                    _db.AddProtocol(questionId, answerId);
                    _db.CompleteTest(testId);
                    return RedirectToAction("Index", "StudentTest");
            }
            return RedirectToAction("Index", "StudentTest");
        }

        
    }
}
