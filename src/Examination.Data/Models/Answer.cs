using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

#nullable disable

namespace Examination.Data.Models
{
    public partial class Answer
    {
        public int Id { get; set; }
        //[DataType(DataType.Text)] - don't work
        [Column(TypeName = "text")]// this works
        public string Text { get; set; }
        public int QuestionId { get; set; }
        [Display(Name = "Is Correct")]
        public bool IsValid { get; set; }

        public virtual Question Question { get; set; }
    }
}
