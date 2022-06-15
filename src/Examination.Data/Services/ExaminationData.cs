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

        public bool EnsureDbCreated() => _db.Database.EnsureCreated();

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
            return _db.Tests
                .Where(t => t.Id == id)
                .Include(t => t.Questions)
                .SingleOrDefault();
        }

        public IEnumerable<Test> GetTests()
        {
            return _db.Tests
                .Select(t => t)
                .Include(t => t.Questions).ThenInclude(q => q.Answers)
                .ToList();
        }

        public void UpdateTest(Test test) 
        {
            //var target = _db.Tests.Where(t => t.Id == test.Id).Include(t => t.Questions).SingleOrDefault();
            //target.Name = test.Name;
            //target.Questions = test.Questions;
            //target.Difficulty = test.Difficulty;

            var entry = _db.Entry(test);
            entry.State = EntityState.Modified;

            _db.SaveChanges();
        }
                
        public IEnumerable<Question> GetQuestionsForTest(int id)
        {
            var questions = _db.Tests.Where(t => t.Id == id)
                .Include(t => t.Questions)
                .SingleOrDefault()
                .Questions.ToList();

            return questions;
        }

        public void AddNewQuestionToTest(Question question, int testId)
        {
            question.Id = 0;
            var test = GetTest(testId);
            test.Questions.Add(question);
            
            _db.SaveChanges();
        }

        public Question GetQuestion(int id)
        {
            return _db.Questions
                .Where(q => q.Id == id)
                .Include(q => q.Answers)
                .SingleOrDefault();
        }

        public void AddNewAnswerToQuestion(Answer answer, int questionId) 
        {
            answer.Id = 0;
            var question = GetQuestion(questionId);
            question.Answers.Add(answer);

            _db.SaveChanges();
        }
    }
}
