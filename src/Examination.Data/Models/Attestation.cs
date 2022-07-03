using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Data.Models
{
    public class Attestation
    {
        public Attestation() 
        {
            Protocols = new HashSet<Protocol>();
        }
        
        public int Id { get; set; }

        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public bool IsActive { get; set; }
        public int TestId { get; set; }
        [StringLength(250)]
        public string UserName { get; set; }

        public Test Test { get; set; }
        public virtual ICollection<Protocol> Protocols { get; set; }
    }
}
