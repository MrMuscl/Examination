using Examination.Data.Models;
using Examination.Data.Services;
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
        private readonly IExaminationData _db;

        public AnswerController(ILogger<AnswerController> logger)
        {
            _logger = logger;
            _db = new ExaminationData(new ExaminationContext());
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add(int? questionId, int? testId ) 
        {
            if (questionId != null) 
            {
                var question = _db.GetQuestion(questionId.Value);
                ViewBag.Question = question;
            }

            if (testId != null)
            {
                var test = _db.GetTest(testId.Value);
                ViewBag.Test = test;
            }

            return View();
        }

        [HttpPost]
        public IActionResult Add(Answer answer, IFormCollection form) 
        {
            int questionId, testId;
            StringValues qValue, tValue;

            if (!form.TryGetValue("QuestionId", out qValue))
            {
                // TODO: handle errors.
                return null; // NotFoundObjectResult;
            }

            if (!form.TryGetValue("TestId", out tValue))
            {
                // TODO: handle errors.
                return null; // NotFoundObjectResult;
            }

            if (!int.TryParse(qValue.ToString(), out questionId))
            {
                // TODO: handle errors.
                return null; // NotFoundObjectResult;
            }

            if (!int.TryParse(tValue.ToString(), out testId))
            {
                // TODO: handle errors.
                return null; // NotFoundObjectResult;
            }

            _db.AddNewAnswerToQuestion(answer, questionId);
            
            return RedirectToAction("Index", "AdminTest", new { Id = testId, questionId = questionId});
        }

        [HttpGet]
        public IActionResult Edit(int id, int questionId, int testId) 
        {
            var model = _db.GetAnswer(id);

            ViewBag.TestId = testId;
            ViewBag.QuestionId = questionId;
            
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Edit(Answer answer, IFormCollection form) 
        {
            int questionId, testId;
            StringValues qValue, tValue;

            if (!form.TryGetValue("QuestionId", out qValue))
            {
                // TODO: handle errors.
                return null; // NotFoundObjectResult;
            }

            if (!form.TryGetValue("TestId", out tValue))
            {
                // TODO: handle errors.
                return null; // NotFoundObjectResult;
            }

            if (!int.TryParse(qValue.ToString(), out questionId))
            {
                // TODO: handle errors.
                return null; // NotFoundObjectResult;
            }

            if (!int.TryParse(tValue.ToString(), out testId))
            {
                // TODO: handle errors.
                return null; // NotFoundObjectResult;
            }

            _db.UpdateAnswer(answer);
            return RedirectToAction("Index", "AdminTest", new { Id = testId, questionId = questionId });
        }

        [HttpGet]
        public IActionResult Remove(int id, int questionId, int testId) 
        {
            ViewBag.QuestionId = questionId;
            ViewBag.TestId = testId;

            var model = _db.GetAnswer(id);
            return View(model);
        }

        [HttpPost]
        public IActionResult Remove(Answer answer, IFormCollection form) 
        {
            int questionId, testId;
            StringValues qValue, tValue;

            if (!form.TryGetValue("QuestionId", out qValue))
            {
                // TODO: handle errors.
                return null; // NotFoundObjectResult;
            }

            if (!form.TryGetValue("TestId", out tValue))
            {
                // TODO: handle errors.
                return null; // NotFoundObjectResult;
            }

            if (!int.TryParse(qValue.ToString(), out questionId))
            {
                // TODO: handle errors.
                return null; // NotFoundObjectResult;
            }

            if (!int.TryParse(tValue.ToString(), out testId))
            {
                // TODO: handle errors.
                return null; // NotFoundObjectResult;
            }
            _db.DeleteAnswer(answer.Id);
            return RedirectToAction("Index", "AdminTest", new { Id = testId, questionId = questionId });
        }


    }
}
