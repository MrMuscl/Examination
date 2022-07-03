using Examination.Data.Models;
using Examination.Data.Services;
using Examination.WEB.Utils;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.WEB.Controllers
{

    [Authorize(Roles = "Administrator, Student")]
    public class StudentTestController : Controller
    {
        private readonly ILogger<StudentTestController> _logger;
        private readonly IExaminationData _examinationDataProvider;
        private readonly UserManager<IdentityUser> _userManager;

        public StudentTestController(ILogger<StudentTestController> logger,
                                     IExaminationData examinationDataProvider,
                                     UserManager<IdentityUser> userManager)
        {
            _logger = logger;
            _examinationDataProvider = examinationDataProvider;
            _userManager = userManager;
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
            IdentityUser user = _userManager.GetUserAsync(HttpContext.User).Result;

            // If questionNumber is null - it means that we are starting a new attestation.
            // Else - we are continue already started attestation proceeding next/prev question.
            if (questionNumber == null) 
            {
                var attestation = new Attestation { UserName = user.UserName};
                attestation.StartTime = DateTime.Now;
                attestation.IsActive = true;
                attestation.TestId = id;
                _examinationDataProvider.AddAttestation(attestation);
            }

            Question question;
            var test = _examinationDataProvider.GetTestWithQuestionsAndAnswers(id);
            ViewBag.UserName = user.UserName;
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
            //ViewBag.CheckedAnswer = question.Protocol?.AnswerId ?? 0;

            var protocol = _examinationDataProvider.GetProtocolForQuestion(question.Id);
            ViewBag.CheckedAnswer = protocol?.AnswerId ?? 0;
            
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

            string userName = "";
            Helper.GetFormStringValue(form, "UserName", out userName);

            switch (navigationBtn) 
            {
                case "Prev":
                    _examinationDataProvider.AddProtocol(questionId, answerId, userName);
                    return RedirectToAction("Execute", "StudentTest", new { Id = testId, QuestionNumber = questionNumber - 1 });
                case "Next":
                    _examinationDataProvider.AddProtocol(questionId, answerId, userName);
                    return RedirectToAction("Execute", "StudentTest", new { Id = testId, QuestionNumber = questionNumber + 1 });
                case "Complete":
                    _examinationDataProvider.AddProtocol(questionId, answerId, userName);
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
