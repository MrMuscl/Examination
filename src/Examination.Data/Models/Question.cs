using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace Examination.Data.Models
{
    public partial class Question
    {
        public Question()
        {
            Answers = new HashSet<Answer>();
            Tests = new HashSet<Test>();
            Protocols = new HashSet<Protocol>();
        }

        public int Id { get; set; }
        //[DataType(DataType.Text)]
        [StringLength(2000)]
        public string Text { get; set; }

        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Test> Tests { get; set; }
        public virtual ICollection<Protocol> Protocols { get; set; }
    }
}
