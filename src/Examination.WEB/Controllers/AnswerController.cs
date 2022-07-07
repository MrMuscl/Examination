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
    public class AnswerController : Controller
    {
        private readonly ILogger<AnswerController> _logger;
        private readonly IExaminationData _examinationDataProvider;

        public AnswerController(ILogger<AnswerController> logger, IExaminationData examinationDataProvider)
        {
            _logger = logger;
            _examinationDataProvider = examinationDataProvider;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add(int questionId, int? testId ) 
        {
            var answer = new Answer();
            var question = _examinationDataProvider.GetQuestion(questionId);
            var test = _examinationDataProvider.GetTest(testId.Value);

            question.Test = test;
            answer.Question = question;

            return View(answer);
        }

        [HttpPost]
        public IActionResult Add(Answer answer, IFormCollection form) 
        {
            int testId;
            Helper.GetFormIntValue(form, "TestId", out testId);

            if (ModelState.IsValid) 
            {
                _examinationDataProvider.AddNewAnswerToQuestion(answer, answer.QuestionId);
                return RedirectToAction("Index", "AdminTest", new { Id = testId, questionId = answer.QuestionId });
            }

            var question = _examinationDataProvider.GetQuestion(answer.QuestionId);
            question.Test = _examinationDataProvider.GetTest(question.TestId);
            answer.Question = question;

            return View(answer);
        }

        [HttpGet]
        public IActionResult Edit(int id, int questionId, int testId) 
        {
            var model = _examinationDataProvider.GetAnswer(id);
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Edit(Answer answer, IFormCollection form) 
        {
            var question = _examinationDataProvider.GetQuestion(answer.QuestionId);

            if (ModelState.IsValid) 
            {
                _examinationDataProvider.UpdateAnswer(answer);
                return RedirectToAction("Index", "AdminTest", new { Id = question.TestId, questionId = answer.QuestionId });
            }
            answer.Question = question;
            return View(answer);
        }

        [HttpGet]
        public IActionResult Remove(int id, int questionId, int testId) 
        {
            var model = _examinationDataProvider.GetAnswer(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Remove(Answer answer, IFormCollection form) 
        {
            var question = _examinationDataProvider.GetQuestion(answer.QuestionId);

            _examinationDataProvider.DeleteAnswer(answer.Id);
            return RedirectToAction("Index", "AdminTest", new { Id = question.TestId, questionId = answer.QuestionId});
        }
    }
}
