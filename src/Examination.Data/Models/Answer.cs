using System;
using System.Collections.Generic;

#nullable disable

namespace Examination.Data.Models
{
    public partial class Answer
    {
        public int Id { get; set; }
        public string Text { get; set; }
        public int QuestionId { get; set; }
        public bool IsValid { get; set; }

        public virtual Question Question { get; set; }
    }
}
