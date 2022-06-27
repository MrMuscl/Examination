using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Examination.Data.Models
{
    public partial class Test
    {
        public Test()
        {
            Questions = new HashSet<Question>();
        }

        public int Id { get; set; }
        [StringLength(250)]
        public string Name { get; set; }
        public TestDifficulty? Difficulty { get; set; }

        public Attestation Attestation { get; set; }
        public virtual ICollection<Question> Questions { get; set; }
    }
}
