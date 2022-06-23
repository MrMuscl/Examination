using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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

        public virtual ICollection<Protocol> Protocols { get; set; }

    }
}
