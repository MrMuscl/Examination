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
    public class AttestationController : Controller
    {

        private readonly ILogger<AttestationController> _logger;
        private readonly IExaminationData _db;

        public AttestationController(ILogger<AttestationController> logger, IExaminationData examinationData)
        {
            _logger = logger;
            _db = examinationData;
        }
        public IActionResult Index()
        {
            var model = _db.GetAttestations();
            return View();
        }
    }
}
