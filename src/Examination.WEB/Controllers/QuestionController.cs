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
    public class QuestionController : Controller
    {
        private readonly ILogger<QuestionController> _logger;
        private readonly IExaminationData _db;

        public QuestionController(ILogger<QuestionController> logger)
        {
            _logger = logger;
            _db = new ExaminationData(new ExaminationContext());
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
            StringValues sTestId;
            int testId = 0;
            if (!form.TryGetValue("TestId", out sTestId)) 
                return RedirectToAction("Index", "AdminTest");
            
            if (!int.TryParse(sTestId, out testId))
                return RedirectToAction("Index", "AdminTest");

            _db.AddNewQuestionToTest(question, testId);
            return RedirectToAction("Index", "AdminTest", new { Id = testId });
        }
    }
}
