using Examination.Data.Models;
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
            var initializer = new ExaminationDbInitializer(_db);
            initializer.SeedTestData();
        }
        
        public bool EnsureDbCreated() => _db.Database.EnsureCreated();

        public async Task AddTest(Test test)
        {
            if (test == null)
            {
                throw new ArgumentNullException("test");
            }
            else 
            {
                _db.Tests.Add(test);
                await _db.SaveChangesAsync();
            }
        }

        public async Task DeleteTest(int id)
        {
            var test = await GetTest(id);
            if (test != null)
            {
                _db.Tests.Remove(test);
                _db.SaveChanges();
            }

        }

        public async Task<Test> GetTest(int id)
        {
            return await _db.Tests
                .Where(t => t.Id == id)
                .Include(t => t.Questions)
                .SingleOrDefaultAsync();
        }
        
        public async Task<Test> GetTestWithQuestions(int id)
        {
            return await _db.Tests
                .Where(t => t.Id == id)
                .Include(t => t.Questions)
                .SingleOrDefaultAsync();
        }

        public async Task<Test> GetTestWithQuestionsAndAnswers(int id) 
        {
            return await _db.Tests
            .Where(t => t.Id == id)
            .Include(t => t.Questions).ThenInclude(q => q.Answers)
            .Include(t => t.Questions).ThenInclude(q => q.Protocols)
            .SingleOrDefaultAsync();
        }

        public async Task<IEnumerable<Test>> GetTests()
        {
            return await _db.Tests
                .Select(t => t)
                .Include(t => t.Questions).ThenInclude(q => q.Answers)
                .ToListAsync();
        }

        public async Task UpdateTest(Test test) 
        {
            //var target = _db.Tests.Where(t => t.Id == test.Id).Include(t => t.Questions).SingleOrDefault();
            //target.Name = test.Name;
            //target.Questions = test.Questions;
            //target.Difficulty = test.Difficulty;

            var entry = _db.Entry(test);
            entry.State = EntityState.Modified;

            await _db.SaveChangesAsync();
        }
                
        public async Task<IEnumerable<Question>> GetQuestionsForTest(int id)
        {
            var tests = await _db.Tests.Where(t => t.Id == id)
                .Include(t => t.Questions)
                .SingleOrDefaultAsync();
           
            return tests.Questions.ToList();
        }

        public async Task AddNewQuestionToTest(Question question, int testId)
        {
            question.Id = 0;
            question.Number = await GetLastQuestionNumber(testId) + 1;
            var test = await GetTest(testId);
            test.Questions.Add(question);
            
            await _db.SaveChangesAsync();
        }

        public async Task<Question> GetQuestion(int id)
        {
            return await _db.Questions
                .Where(q => q.Id == id)
                .Include(q => q.Answers)
                .Include(q => q.Test)
                .SingleOrDefaultAsync();
        }

        public async Task AddNewAnswerToQuestion(Answer answer, int questionId) 
        {
            answer.Id = 0;
            var question = await GetQuestion(questionId);
            question.Answers.Add(answer);

            await _db.SaveChangesAsync();
        }

        public async Task DeleteQuestion(int questionId) 
        {
            var question = await _db.Questions
                .Include(q => q.Answers)
                .Include(q => q.Test)
                .Where(q => q.Id == questionId)
                .SingleOrDefaultAsync();
            
            var testId = question.Test.Id;

            _db.Questions.Remove(question);
            await _db.SaveChangesAsync();

            // Adjust question number after removing one.
            var test = await _db.Tests.Where(t => t.Id == testId).Include(t => t.Questions).FirstOrDefaultAsync();
            var questions = test.Questions;
            for (int i = 0; i < questions.Count(); i++) 
            {
                questions.ToArray()[i].Number = i + 1;
            }
            await _db.SaveChangesAsync();
        }

        public async Task UpdateQuestion(Question question) 
        {
            var entry = _db.Entry(question);
            entry.State = EntityState.Modified;

            await _db.SaveChangesAsync();
        }

        public async Task<Answer> GetAnswer(int id) 
        {
            return await _db.Answers
                .Include(a => a.Question)
                .Where(a => a.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task UpdateAnswer(Answer answer) 
        {
            var target = await _db.Answers.Where(a => a.Id == answer.Id).SingleOrDefaultAsync();
            target.Text = answer.Text;
            target.IsValid = answer.IsValid;

            //var entry = _db.Entry(answer);
            //entry.State = EntityState.Modified;

            await _db.SaveChangesAsync();
        }

        public async Task DeleteAnswer(int answerId) 
        {
            var answer = await _db.Answers
                .Where(a => a.Id == answerId)
                .SingleOrDefaultAsync();

            _db.Answers.Remove(answer);
            await _db.SaveChangesAsync();
        }

        public async Task<int> GetLastQuestionNumber(int testId) 
        {
            //return _db.Questions.Where(q => q.Test.Id == testId).Max(q => q.Number);
            return await _db.Questions
                .Where(q => q.Test.Id == testId)
                .OrderByDescending(q => q.Number)
                .Select(q => q.Number)
                .FirstOrDefaultAsync();
        }

        public async Task<int> AddProtocol(int questionId, int answerId, string userName) 
        {
            Question question = null;
            Answer answer = null;
            Attestation attestation = null;
            Protocol protocol = null;

            var questionTask = _db.Questions
                .Where(q => q.Id == questionId)
                .Include(q => q.Protocols)
                .FirstOrDefaultAsync();
            
            var answerTask = _db.Answers
                .Where(a => a.Id == answerId)
                .FirstOrDefaultAsync();

            var attestationTask = _db.Attestations
                .Where(a => a.IsActive == true && a.UserName == userName)
                .Include(a => a.Protocols)
                .OrderByDescending(a => a.StartTime)
                .FirstOrDefaultAsync();
                        
            var tasks = new List<Task> { questionTask, answerTask, attestationTask };
            while (tasks.Count > 0) 
            {
                var completedTask = await Task.WhenAny(tasks);
                if (completedTask == answerTask)
                {
                    answer = await answerTask;
                    if (answer == null)
                        return 0;
                }
                else if (completedTask == questionTask)
                {
                    question = await questionTask;
                    if (question == null)
                        throw new NullReferenceException("Question");
                }
                else if (completedTask == attestationTask)
                {
                    attestation = await attestationTask;
                    protocol = attestation?.Protocols.Where(p => p.QuestionId == questionId).FirstOrDefault();
                }
                tasks.Remove(completedTask);
            }

            // If there is no protocol for current user in active attestation for this question - create one...
            if (protocol == null)
            {
                protocol = new Protocol 
                {
                    Question = question,
                    Answer = answer,
                    Attestation = await GetLastActiveAttestation(userName) 
                };
                _db.Protocols.Add(protocol);
            }
            else 
            {
                // ... otherwise update existing protocol.
                protocol.AnswerId = answerId;
            }
                        
            await _db.SaveChangesAsync();
            return GetLastActiveAttestation(userName).Id;
        }

        public async Task AddAttestation(Attestation attestation) 
        {
            // Remove all active attestations
            var activeAttestations = await _db.Attestations.Where(a => a.IsActive == true).ToListAsync();
            _db.Attestations.RemoveRange(activeAttestations);
            
            await _db.Attestations.AddAsync(attestation);
            await _db.SaveChangesAsync();
        }

        public async Task<Attestation> GetLastActiveAttestation(string userName) 
        {
            return _db.Attestations
                .Where(a => a.IsActive == true && a.UserName == userName)
                .OrderByDescending(a => a.StartTime)
                .FirstOrDefault();
        }
                
        public async Task<int> CompleteTest(int testId, string userName)
        {
            var attestation = await GetLastActiveAttestation(userName);
            attestation.EndTime = DateTime.Now;
            attestation.IsActive = false;

            var test = await _db.Tests
                .Where(t => t.Id == testId)
                .Include(t => t.Questions)
                .ThenInclude(q => q.Protocols)
                .SingleOrDefaultAsync();
                
            var notCompleted = test.Questions.Where(q => q.Protocols == null).ToList();

            if (notCompleted.Count() > 0) 
            {
                return notCompleted.First().Number;
            }
           
            await _db.SaveChangesAsync();
            
            return 0;
        }

        public async Task<IEnumerable<Attestation>> GetAttestations() 
        {
            return await _db.Attestations.Include(a => a.Protocols).ToListAsync();
        }

        public async Task<Attestation> GetAttestation(int id) 
        {
            return await _db.Attestations.Where(a => a.Id == id).Include(a => a.Protocols).SingleOrDefaultAsync();
        }

        public async Task<Attestation> GetAttestationWithQuestionsAndAnswers(int id)
        {
            return await _db.Attestations
                .Where(a => a.Id == id)
                .Include(a =>a.Test)
                .Include(a => a.Protocols)
                .ThenInclude(p=>p.Question)
                .ThenInclude(p=>p.Answers)
                .SingleOrDefaultAsync();
        }
        public async Task<Protocol> GetProtocolForQuestion(int questionId, string userName)
        {
            var questionTask = _db.Questions
                .Where(q => q.Id == questionId)
                .Include(q => q.Protocols)
                .ThenInclude(p=>p.Attestation)
                .SingleOrDefaultAsync();
            
            var attestationTask = GetLastActiveAttestation(userName);

            var question = await questionTask;
            var attestation = await attestationTask;

            return question.Protocols.Where(p => p?.Attestation?.Id == attestation.Id).FirstOrDefault();
        }
    }
}
