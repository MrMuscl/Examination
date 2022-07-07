using Examination.Data.Models;
using Examination.Data.Services;
using Examination.WEB.Models;
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
        private readonly IExaminationData _examinationDataProvider;

        public QuestionController(ILogger<QuestionController> logger, IExaminationData examinationDataProvider)
        {
            _logger = logger;
            _examinationDataProvider = examinationDataProvider;
        }
        public IActionResult Index()
        {
            return View();
        }

        [HttpGet]
        public IActionResult Add(int id) 
        {
            var test = _examinationDataProvider.GetTest(id);
            var question = new Question();
            question.Test = test;
            
            return View(question);
        }
        [HttpPost]
        public IActionResult Add(Question question, IFormCollection form)
        {
            if (ModelState.IsValid) 
            {
                _examinationDataProvider.AddNewQuestionToTest(question, question.TestId);
                return RedirectToAction("Index", "AdminTest", new { Id = question.TestId });
            }

            var test = _examinationDataProvider.GetTest(question.TestId);
            question.Test = test;

            return View(question);
        }

        [HttpGet]
        public IActionResult Remove(int id, int? testId) 
        {
            var model = _examinationDataProvider.GetQuestion(id);
            return View(model);
        }
        
        [HttpPost]
        public IActionResult Remove(Question question, IFormCollection form) 
        {
            int testId = _examinationDataProvider.GetQuestion(question.Id).TestId;
            _examinationDataProvider.DeleteQuestion(question.Id);
            
            return RedirectToAction("Index", "Admintest", new { id = testId});
        }

        [HttpGet]
        public IActionResult Edit(int id, int? testId)
        {
            var question = _examinationDataProvider.GetQuestion(id);
           
            return View(question);
        }

        public IActionResult Edit(Question question, IFormCollection form)
        {
            if (ModelState.IsValid) 
            {
                _examinationDataProvider.UpdateQuestion(question);
                return RedirectToAction("Index", "AdminTest", new { id = question.TestId });
            }
            
            return View(question);
        }
    }
}
