using Examination.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.WEB.Models
{
    public class EditTestViewModel
    {
        public Test Test { get; set; }
        public string Field { get; set; }
        public IEnumerable<Question> questions;
    }
}
