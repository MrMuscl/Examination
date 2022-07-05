using Examination.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.WEB.Models
{
    public class TestResultsViewMode
    {
        public Test Test { get; set; }
        public Attestation Attestation { get; set; }
        public bool IsPassed { get; set; }
        public int CorrectCount { get; set; }
        public int IncorrectCount { get; set; }
    }
}
