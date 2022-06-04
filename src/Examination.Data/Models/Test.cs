using System;
using System.Collections.Generic;

#nullable disable

namespace Examination.Data.Models
{
    public partial class Test
    {
        public Test()
        {
            TestQuestions = new HashSet<TestQuestion>();
        }

        public int Id { get; set; }
        public string Name { get; set; }
        public int? Difficulty { get; set; }

        public virtual ICollection<TestQuestion> TestQuestions { get; set; }
    }
}
