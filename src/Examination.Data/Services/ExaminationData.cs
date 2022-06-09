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
            return _db.Tests.Select(t => t).Include(t => t.TestQuestions).ToList();
        }

        public void UpdateTest(Test test) 
        {
            var entry = _db.Entry(test);
            entry.State = EntityState.Modified;
            _db.SaveChanges();
        }

        //public IEnumerable<string> GetQuestionTextForTest(int id)
        //{
        //    var questions = _db.Questions.Join(_db.TestQuestions,
        //                                        quest => quest.Id,
        //                                        qt => qt.QuestionId,
        //                                        (quest, qt) =>
        //                                        new { QuestionText = quest.Text, TestId = qt.TestId }).Where(qt => qt.TestId == id).ToList();
        //    var res = new List<string>();
        //    foreach (var itm in questions)
        //        res.Add(itm.QuestionText);

        //    return res;
        //}

        public IEnumerable<Question> GetQuestionsForTest(int id)
        {
            var questions = _db.TestQuestions
                .Include(tq => tq.Question)
                .Where(tq => tq.TestId == 1)
                .Select(tq => tq.Question).ToList();

            return questions;
        }
    }
}
