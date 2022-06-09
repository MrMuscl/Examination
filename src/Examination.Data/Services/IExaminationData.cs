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
        /// Get all test objects
        /// </summary>
        /// <returns>The collection of tests</returns>
        IEnumerable<Test> GetTests();

        /// <summary>
        /// Get test with specified id
        /// </summary>
        /// <param name="">Test id</param>
        /// <returns>Test object </returns>
        Test GetTest(int id);

        /// <summary>
        /// Add test 
        /// </summary>
        /// <param name="test">Test object that should be added</param>
        void AddTest(Test test);

        /// <summary>
        /// Get questions for specified test
        /// </summary>
        /// <param name="id">Test id</param>
        /// <returns>The collection of question objects</returns>
        IEnumerable<Question> GetQuestionsForTest(int id);

        /// <summary>
        /// Delete test with specified id
        /// </summary>
        /// <param name="id">Test id</param>
        void DeleteTest(int id);

        /// <summary>
        /// Updated test with new object
        /// </summary>
        /// <param name="id">Test object</param>
        void UpdateTest(Test test);
        
            
        
    }
}
