using Examination.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Data.Services
{
    public class ExaminationData : IExaminationData
    {
        ExaminationContext _db;

        public ExaminationData(ExaminationContext db)
        {
            _db = db;
        }

        public void AddTest(Test test)
        {
            if (test == null)
            {
                throw new ArgumentNullException("test");
            }
            else 
            {
                _db.Tests.Add(test);
                _db.SaveChanges();
            }
        }

        public void DeleteTest(int id)
        {
            var test = GetTest(id);
            if (test != null)
            {
                _db.Tests.Remove(test);
                _db.SaveChanges();
            }

        }

        public Test GetTest(int id)
        {
            return _db.Tests.Find(id);
        }

        public IEnumerable<Test> GetTests()
        {
            return _db.Tests.Select(t => t).ToList();
        }

        public IEnumerable<Test> GetTestsWithAnswers()
        {
            return _db.Tests.Select(t => t)
                .Include(t => t.TestQuestions)
                .ToList();
        }
    }
}
