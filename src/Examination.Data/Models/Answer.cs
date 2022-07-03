using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Examination.Data.Models
{
    public partial class Answer
    {
        public Answer() 
        {
            Protocols = new HashSet<Protocol>();
        }
        public int Id { get; set; }
        //[DataType(DataType.Text)] - don't work
        //[Column(TypeName = "text")]// this works
        [StringLength(2000)]
        public string Text { get; set; }
        [Display(Name = "Is Correct")]
        public bool IsValid { get; set; }
        public int QuestionId { get; set; }
        
        public virtual Question Question { get; set; }
        public virtual IEnumerable<Protocol> Protocols { get; set; }
    }
}
