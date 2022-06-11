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
            Tests = new HashSet<Test>();
        }

        public int Id { get; set; }
        public string Text { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
    }
}
