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
        private readonly IExaminationData _examinationDataProvider;

        public AttestationController(ILogger<AttestationController> logger, IExaminationData examinationDataProvider)
        {
            _logger = logger;
            _examinationDataProvider = examinationDataProvider;
        }
        
        [HttpGet]
        public async Task<IActionResult> Index()
        {
            List<AttestationItemViewModel> model = new List<AttestationItemViewModel>();
            var attestations = await _examinationDataProvider.GetAttestations();
            foreach (var attestation in attestations) 
            {
                var test = await _examinationDataProvider.GetTest(attestation.TestId);
                var item = new AttestationItemViewModel { Attestation = attestation, Test = test };
                model.Add(item);
            }

            return View(model);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id) 
        {
            var model = await Utils.AttestationDetailsModelBuilder.Build(_examinationDataProvider, id);
            
            return View(model);
        }
    }
}
