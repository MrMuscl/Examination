﻿using Examination.Data.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.Data.Services
{
    public class ExaminationData : IExaminationData, IDisposable
    {
        ExaminationContext _db;
        private bool _disposed = false;

        public ExaminationContext GetExaminationContext { get { return _db; } }
        public ExaminationData(ExaminationContext db)
        {
            _db = db;
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        ~ExaminationData() 
        {
            Dispose(false);
        }

        void Dispose(bool fromDispose) 
        {
            if (!_disposed) 
            {
                // Dispose all managed resources.
                if (fromDispose) 
                {
                    _db.Dispose();
                }
                // Disposed all unmanaged resourses here.
            }

            _disposed = true;
        }

        public void SeedTestData() 
        {
            var initializer = new ExaminationDbInitializer();
            initializer.SeedTestData();
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
        
        public Test GetTestWithQuestions(int id)
        {
            return _db.Tests
                .Where(t => t.Id == id)
                .Include(t => t.Questions)
                .SingleOrDefault();
        }

        public Test GetTestWithQuestionsAndAnswers(int id) 
        {
            return _db.Tests
            .Where(t => t.Id == id)
            .Include(t => t.Questions).ThenInclude(q => q.Answers)
            .Include(t => t.Questions).ThenInclude(q => q.Protocols)
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
            question.Number = GetLastQuestionNumber(testId) + 1;
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

        public void DeleteQuestion(int questionId) 
        {
            var question = _db.Questions
                .Include(q => q.Answers)
                .Include(q => q.Test)
                .Where(q => q.Id == questionId)
                .SingleOrDefault();
            
            var testId = question.Test.Id;

            _db.Questions.Remove(question);
            _db.SaveChanges();

            // Adjust question number after removing one.
            var questions = _db.Tests.Where(t => t.Id == testId).Include(t => t.Questions).FirstOrDefault().Questions;
            for (int i = 0; i < questions.Count(); i++) 
            {
                questions.ToArray()[i].Number = i + 1;
            }
            _db.SaveChanges();
        }

        public void UpdateQuestion(Question question) 
        {
            var entry = _db.Entry(question);
            entry.State = EntityState.Modified;

            _db.SaveChanges();
        }

        public Answer GetAnswer(int id) 
        {
            return _db.Answers
                .Include(a => a.Question)
                .Where(a => a.Id == id)
                .FirstOrDefault();
        }

        public void UpdateAnswer(Answer answer) 
        {
            var entry = _db.Entry(answer);
            entry.State = EntityState.Modified;

            _db.SaveChanges();
        }

        public void DeleteAnswer(int answerId) 
        {
            var answer = _db.Answers
                .Where(a => a.Id == answerId)
                .SingleOrDefault();

            _db.Answers.Remove(answer);
            _db.SaveChanges();
        }

        public int GetLastQuestionNumber(int testId) 
        {
            //return _db.Questions.Where(q => q.Test.Id == testId).Max(q => q.Number);
            return  _db.Questions
                .Where(q => q.Test.Id == testId)
                .OrderByDescending(q => q.Number)
                .Select(q => q.Number)
                .FirstOrDefault();
        }

        public void AddProtocol(int questionId, int answerId, string userName) 
        {
            var question = _db.Questions.Where(q => q.Id == questionId).Include(q => q.Protocols).FirstOrDefault();
            var answer = _db.Answers.Where(a => a.Id == answerId).FirstOrDefault();
            if (answer == null)
                return;

            if (question == null)
                throw new NullReferenceException("Question");

            // If there is no protocol for current user in active attestation for this question - create one...
            var protocol = _db.Attestations
                .Where(a => a.IsActive == true && a.UserName == userName)
                .Include(a => a.Protocols)
                .OrderByDescending(a => a.StartTime)
                .FirstOrDefault()?.Protocols.Where(p => p.QuestionId == questionId).FirstOrDefault();

            if (protocol == null)
            {
                protocol = new Protocol { Question = question, Answer = answer, Attestation = GetLastActiveAttestation() };
                _db.Protocols.Add(protocol);
            }
            else 
            {
                // ... otherwise update existing protocol.
                protocol.AnswerId = answerId;
            }
                        
            _db.SaveChanges();
        }

        public void AddAttestation(Attestation attestation) 
        {
            // Remove all active attestations
            var activeAttestations = _db.Attestations.Where(a => a.IsActive == true).ToList();
            _db.Attestations.RemoveRange(activeAttestations);
            
            _db.Attestations.Add(attestation);
            _db.SaveChanges();
        }

        public Attestation GetLastActiveAttestation() 
        {
            return _db.Attestations.Where(a => a.IsActive == true).OrderByDescending(a => a.StartTime).FirstOrDefault();
        }
                
        public  int CompleteTest(int testId)
        {
            var attestation = GetLastActiveAttestation();
            attestation.EndTime = DateTime.Now;
            attestation.IsActive = false;

            var notCompleted = _db.Tests
                .Where(t => t.Id == testId)
                .Include(t => t.Questions)
                .ThenInclude(q => q.Protocols)
                .SingleOrDefault()
                .Questions.Where(q => q.Protocols == null).ToList();

            if (notCompleted.Count() > 0) 
            {
                return notCompleted.First().Number;
            }

            //attestation.Protocols = protocols;
           
            _db.SaveChanges();
            
            return 0;
        }

        public IEnumerable<Attestation> GetAttestations() 
        {
            return _db.Attestations.Include(a => a.Protocols).ToList();
        }

        public Attestation GetAttestation(int id) 
        {
            return _db.Attestations.Where(a => a.Id == id).Include(a => a.Protocols).SingleOrDefault();
        }

        public Attestation GetAttestationWithQuestionsAndAnswers(int id)
        {
            return _db.Attestations
                .Where(a => a.Id == id)
                .Include(a =>a.Test)
                .Include(a => a.Protocols)
                .ThenInclude(p=>p.Question)
                .ThenInclude(p=>p.Answers)
                .SingleOrDefault();
        }
        public Protocol GetProtocolForQuestion(int questionId)
        {
            var question = _db.Questions.Where(q => q.Id == questionId).Include(q => q.Protocols).SingleOrDefault();
            var attestation = GetLastActiveAttestation();

            return question.Protocols.Where(p => p.Attestation.Id == attestation.Id).FirstOrDefault();
        }
    }
}
