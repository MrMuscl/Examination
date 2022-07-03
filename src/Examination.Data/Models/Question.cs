using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

#nullable disable

namespace Examination.Data.Models
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            Protocols = new HashSet<Protocol>();
        }

        public int Id { get; set; }
        [StringLength(2000)]
        public string Text { get; set; }
        public int Number { get; set; }
        public int TestId { get; set; }

        public virtual Test Test { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Protocol> Protocols { get; set; }

        [NotMapped]
        public string CorrectAnswer 
        {
            get 
            {
                return this.Answers.Where(a => a.IsValid == true).FirstOrDefault()?.Text ?? "";
            }
        }
    }
}
