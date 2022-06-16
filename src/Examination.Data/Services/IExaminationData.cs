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
        IEnumerable<Test> GetTests();

        /// <summary>
        /// Get test with specified id.
        /// </summary>
        /// <param name="">Test id</param>
        /// <returns>Test object </returns>
        Test GetTest(int id);

        /// <summary>
        /// Add test. 
        /// </summary>
        /// <param name="test">Test object that should be added</param>
        void AddTest(Test test);

        /// <summary>
        /// Get questions for specified test.
        /// </summary>
        /// <param name="id">Test id</param>
        /// <returns>The collection of question objects</returns>
        IEnumerable<Question> GetQuestionsForTest(int id);

        /// <summary>
        /// Delete test with specified id.
        /// </summary>
        /// <param name="id">Test id</param>
        void DeleteTest(int id);

        /// <summary>
        /// Update test with new object.
        /// </summary>
        /// <param name="id">Test object</param>
        void UpdateTest(Test test);

        /// <summary>
        /// Add new question to specified test.
        /// </summary>
        /// <param name="question">Question object</param>
        /// <param name="testId">Test id the question should be added to</param>
        void AddNewQuestionToTest(Question question, int testId);

        /// <summary>
        /// Get question with specifid id
        /// </summary>
        /// <param name="id">Question id</param>
        /// <returns>Question object</returns>
        Question GetQuestion(int id);

        /// <summary>
        /// Add new answer to specified test.
        /// </summary>
        /// <param name="answer">Answer object</param>
        /// <param name="questionId">Question id the answer should be added to</param>
        void AddNewAnswerToQuestion(Answer answer, int questionId);

        /// <summary>
        /// Remove question enity with all related answers.
        /// </summary>
        /// <param name="questionId">Id of the question</param>
        void DeleteQuestion(int questionId);

        /// <summary>
        /// Update question with new object.
        /// </summary>
        /// <param name="question">Question object</param>
        void UpdateQuestion(Question question);

        /// <summary>
        /// Get answer with specified id.
        /// </summary>
        /// <param name="id">Answer id</param>
        /// <returns>Answer object</returns>
        Answer GetAnswer(int id);

        /// <summary>
        /// Updaet answer with new object.
        /// </summary>
        /// <param name="answer">Answer object</param>
        void UpdateAnswer(Answer answer);
     }
}
