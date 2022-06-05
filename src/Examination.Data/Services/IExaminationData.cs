using Examination.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Data.Services
{
    public interface IExaminationData
    {
        IEnumerable<Test> GetTests();
        IEnumerable<Test> GetTestsWithAnswers();
        void AddTest(Test test);
    }
}
