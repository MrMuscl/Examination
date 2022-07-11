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
        /// <summary>
        /// Get all test objects.
        /// </summary>
        /// <returns>The collection of tests</returns>
        Task<IEnumerable<Test>> GetTests();

        /// <summary>
        /// Get test with specified id.
        /// </summary>
        /// <param name="">Test id</param>
        /// <returns>Test object </returns>
        Task<Test> GetTest(int id);
        
        /// <summary>
        /// Get test with specified id include related questions.
        /// </summary>
        /// <param name="id">Test id</param>
        /// <returns>Test object </returns>
        Task<Test> GetTestWithQuestions(int id);

        /// <summary>
        /// Get test with specified id include related questions and answers.
        /// </summary>
        /// <param name="id">Test id</param>
        /// <returns>Test object </returns>
        Task<Test> GetTestWithQuestionsAndAnswers(int id);

        /// <summary>
        /// Add test. 
        /// </summary>
        /// <param name="test">Test object that should be added</param>
        Task AddTest(Test test);

        /// <summary>
        /// Get questions for specified test.
        /// </summary>
        /// <param name="id">Test id</param>
        /// <returns>The collection of question objects</returns>
        Task<IEnumerable<Question>> GetQuestionsForTest(int id);

        /// <summary>
        /// Delete test with specified id.
        /// </summary>
        /// <param name="id">Test id</param>
        Task DeleteTest(int id);

        /// <summary>
        /// Update test with new object.
        /// </summary>
        /// <param name="id">Test object</param>
        Task UpdateTest(Test test);

        /// <summary>
        /// Add new question to specified test.
        /// </summary>
        /// <param name="question">Question object</param>
        /// <param name="testId">Test id the question should be added to</param>
        Task AddNewQuestionToTest(Question question, int testId);

        /// <summary>
        /// Get question with specifid id
        /// </summary>
        /// <param name="id">Question id</param>
        /// <returns>Question object</returns>
        Task<Question> GetQuestion(int id);

        /// <summary>
        /// Add new answer to specified test.
        /// </summary>
        /// <param name="answer">Answer object</param>
        /// <param name="questionId">Question id the answer should be added to</param>
        Task AddNewAnswerToQuestion(Answer answer, int questionId);

        /// <summary>
        /// Remove question enity with all related answers.
        /// </summary>
        /// <param name="questionId">Id of the question</param>
        Task DeleteQuestion(int questionId);

        /// <summary>
        /// Update question with new object.
        /// </summary>
        /// <param name="question">Question object</param>
        Task UpdateQuestion(Question question);

        /// <summary>
        /// Get answer with specified id.
        /// </summary>
        /// <param name="id">Answer id</param>
        /// <returns>Answer object</returns>
        Task<Answer> GetAnswer(int id);
        
        /// <summary>
        /// Updaet answer with new object.
        /// </summary>
        /// <param name="answer">Answer object</param>
        Task UpdateAnswer(Answer answer);

        /// <summary>
        /// Remove answer entity.
        /// </summary>
        /// <param name="answerId">Answer id</param>
        Task DeleteAnswer(int answerId);

        /// <summary>
        /// Add new protocol. If protocol already exists - then update it.
        /// </summary>
        /// <param name="questionId">Question id</param>
        /// <param name="answerId">Answer id</param>
        /// <param name="userName">Current user name</param>
        /// <returns>The id of attestation that owns the protocol</returns>
        Task<int> AddProtocol(int questionId, int answerId, string userName);

        /// <summary>
        /// Add new attestation.
        /// </summary>
        /// <param name="attestation">Attestation object</param>
        Task AddAttestation(Attestation attestation);

        /// <summary>
        /// Complete test. Add protocols collection to active attestation and store end date.
        /// </summary>
        /// <param name="testId">Test id</param>
        /// <param name="userName">The name of the user, that executes test</param>
        /// If there are not aswered questions, returns the Id on the first one.
        Task<int> CompleteTest(int testId, string userName);

        /// <summary>
        /// Get attestation list
        /// </summary>
        Task<IEnumerable<Attestation>> GetAttestations();

        /// <summary>
        /// Get Attestation.
        /// </summary>
        /// <param name="id">Attestation id</param>
        /// <returns>Attestation object</returns>
        Task<Attestation> GetAttestation(int id);

        /// <summary>
        /// Get Attestation with related questions and answers.
        /// </summary>
        /// <param name="id">Attestation id</param>
        /// <returns>Attestation object</returns>
        Task<Attestation> GetAttestationWithQuestionsAndAnswers(int id);
        
        /// <summary>
        /// Get protocol for specified question.
        /// </summary>
        /// <param name="questionId">Question Id</param>
        /// <returns>Protocol object</returns>
        Task<Protocol> GetProtocolForQuestion(int questionId, string userName);
    }
}
