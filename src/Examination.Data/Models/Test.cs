using System;
using System.Collections.Generic;

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
        public string Name { get; set; }
        public TestDifficulty? Difficulty { get; set; }

        public virtual ICollection<Question> Questions { get; set; }
    }
}
