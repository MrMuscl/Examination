using System;
using System.Collections.Generic;

#nullable disable

namespace Examination.Data.Models
{
    public partial class TestQuestion
    {
        public int TestId { get; set; }
        public int QuestionId { get; set; }

        public virtual Question Question { get; set; }
        public virtual Test Test { get; set; }
    }
}
