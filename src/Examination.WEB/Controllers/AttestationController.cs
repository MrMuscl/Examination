using Examination.Data.Models;
using Examination.Data.Services;
using Examination.WEB.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.WEB.Controllers
{
    public class AttestationController : Controller
    {

        private readonly ILogger<AttestationController> _logger;
        private readonly IExaminationData _db;

        public AttestationController(ILogger<AttestationController> logger, IExaminationData examinationData)
        {
            _logger = logger;
            _db = examinationData;
        }
        
        [HttpGet]
        public IActionResult Index()
        {
            List<AttestationItemViewModel> model = new List<AttestationItemViewModel>();
            var attestations = _db.GetAttestations();
            foreach (var attestation in attestations) 
            {
                var test = _db.GetTest(attestation.TestId);
                var item = new AttestationItemViewModel { Attestation = attestation, Test = test };
                model.Add(item);
            }

            return View(model);
        }

        [HttpGet]
        public IActionResult Details(int id) 
        {
            Attestation attestation = _db.GetAttestationWithQuestionsAndAnswers(id);
            var model = attestation.Protocols;
            ViewBag.TestName = attestation.Test?.Name;
            
            return View(model);
        }
    }
}
