﻿using Examination.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Data.Services
{
    interface IExaminationData
    {
        IEnumerable<Test> GetTests();
    }
}
