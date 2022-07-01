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
        private readonly IExaminationData _examinationDataProvider;

        public StudentTestController(ILogger<StudentTestController> logger, IExaminationData examinationDataProvider)
        {
            _logger = logger;
            _examinationDataProvider = examinationDataProvider;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var model = _examinationDataProvider.GetTests();
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
                attestation.TestId = id;
                _examinationDataProvider.AddAttestation(attestation);
            }

            Question question;
            var test = _examinationDataProvider.GetTestWithQuestionsAndAnswers(id);
            ViewBag.QuestionNumber = questionNumber?? 1;
            ViewBag.QuestionCount = test.Questions.Count();
            ViewBag.TestId = id;

            if (questionNumber == null)
            {
                question = test.Questions.OrderBy(q => q.Number).FirstOrDefault();
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
                    _examinationDataProvider.AddProtocol(questionId, answerId);
                    return RedirectToAction("Execute", "StudentTest", new { Id = testId, QuestionNumber = questionNumber - 1 });
                case "Next":
                    _examinationDataProvider.AddProtocol(questionId, answerId);
                    return RedirectToAction("Execute", "StudentTest", new { Id = testId, QuestionNumber = questionNumber + 1 });
                case "Complete":
                    _examinationDataProvider.AddProtocol(questionId, answerId);
                    int noAnswerNumber = _examinationDataProvider.CompleteTest(testId);
                    if (noAnswerNumber > 0)
                        return RedirectToAction("Execute", "StudentTest", new { Id = testId, QuestionNumber = noAnswerNumber });
                    else
                        return RedirectToAction("Index", "StudentTest");
            }
            return RedirectToAction("Index", "StudentTest");
        }
    }
}
