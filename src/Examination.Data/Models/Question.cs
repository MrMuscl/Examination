using System;
using System.Collections.Generic;

#nullable disable

namespace Examination.Data.Models
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            TestQuestions = new HashSet<TestQuestion>();
        }

        public int Id { get; set; }
        public string Text { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<TestQuestion> TestQuestions { get; set; }
    }
}
