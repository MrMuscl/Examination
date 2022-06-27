using Examination.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.WEB.Models
{
    public class AttestationItemViewModel
    {
        public Attestation Attestation { get; set; } 
        public Test Test { get; set; }
    }
}
