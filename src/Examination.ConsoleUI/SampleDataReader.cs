using Examination.Data.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.ConsoleUI
{
    public class SampleDataReader
    {
        private List<string> _files;
        private string _dataFolder = "";
        private ExaminationContext _db;

        public SampleDataReader()
        {
            _dataFolder = Path.GetFullPath(Path.Combine(Environment.CurrentDirectory, @"..\..\..\..\..\DB\Data\Industry_light"));
            _files = new List<string>(Directory.GetFiles(_dataFolder));
            _db = new ExaminationContext();
        }

        public List<Question> ParseQuestions(string path)
        {
            var lines = File.ReadAllLines(path);
            var result = new List<Question>();
            var question = new Question();
            var answer = new Answer();
            int questionNumber = 1;

            for (int i = 0; i < lines.Count() - 1; i++) 
            {
                if (lines[i].StartsWith('#'))
                {
                    question = new Question { Text = lines[i].Substring(1), Number = questionNumber };
                    continue;
                }
                if (lines[i].StartsWith('+') || lines[i].StartsWith('-')) 
                {
                    answer = new Answer { Text = lines[i].Substring(1)};
                    answer.IsValid = lines[i].StartsWith('+');
                    question.Answers.Add(answer);
                }
                if (lines[i].StartsWith('&')) 
                {
                    result.Add(question);
                    questionNumber++;
                }
                
            }
            
            return result;
        }

        public void SeedTestData() 
        {
            foreach (var file in _files) 
            {
                string testName = Path.GetFileNameWithoutExtension(file);
                var questions = ParseQuestions(file);
                var test = new Test { Name = testName, Difficulty = TestDifficulty.Easy };
                test.Questions = questions;
                
                _db.Tests.Add(test);
                _db.SaveChanges();
            }
        }

        


    }
}
