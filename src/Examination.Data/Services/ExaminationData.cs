﻿using Examination.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Data.Services
{
    public class ExaminationData : IExaminationData
    {
        public IEnumerable<Test> GetTests()
        {
            throw new NotImplementedException();
        }
    }
}
