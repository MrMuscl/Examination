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
        /// Get all test objects with all related questions.
        /// </summary>
        /// <returns>The collection of tests</returns>
        IEnumerable<Test> GetTestsWithAnswers();
        
        /// <summary>
        /// Add test 
        /// </summary>
        /// <param name="test">Test object that should be added</param>
        void AddTest(Test test);
        
        /// <summary>
        /// Get test with scpecified id
        /// </summary>
        /// <param name="">Test id</param>
        /// <returns>Test object </returns>
        Test GetTest(int id);

        /// <summary>
        /// Delete test with specified id
        /// </summary>
        /// <param name="id">Test id</param>
        void DeleteTest(int id);
        
            
        
    }
}
