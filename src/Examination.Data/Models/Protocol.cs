using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Data.Models
{
    public class Protocol
    {
        public int Id {get; set;}
        public int TestId { get; set; }
        public int QuestionId { get; set; }
        public int AnswerId { get; set; }
        public int? AttestationId { get; set; }

        public virtual Attestation Attestation { get; set; }
        public virtual Answer Answer { get; set; }
        public virtual Question Question { get; set; }
        public virtual Test Test { get; set; }

    }
}
