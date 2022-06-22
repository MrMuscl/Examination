using Examination.Data.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Examination.ConsoleUI
{
    public class InitDatabase
    {
        private ExaminationContext _db;

        public InitDatabase(ExaminationContext context)
        {
            _db = context;
        }

        public bool EnsureDbCreated() => _db.Database.EnsureCreated();
        public bool EnsureDbDeleted() => _db.Database.EnsureDeleted();

        public void SeedTestData() 
        {
            var test1 = new Test 
            { 
                Name = "Astronomy",
                Difficulty = TestDifficulty.Easy,
                Questions = new List<Question> 
                {
                    new Question
                    { Text = "What is the distance from the Earth to the Moon?", Answers = new List<Answer>
                        { 
                            new Answer{ Text="100 000 km", IsValid = false},
                            new Answer{ Text="250 000 km", IsValid = false},
                            new Answer{ Text="350 000 km", IsValid = true}
                        }, Number = 1 
                    },
                    new Question
                    { Text = "What is the largest planet in Solar system?", Answers = new List<Answer>
                        {
                            new Answer{ Text = "Jupiter", IsValid = true},
                            new Answer{ Text = "Earth", IsValid = false},
                            new Answer{ Text = "Mars", IsValid = false}
                        }, Number = 2
                    }
                }
            };

            var test2 = new Test
            {
                Name = "Physics",
                Difficulty = TestDifficulty.Hard,
                Questions = new List<Question>
                {
                    
                }
            };

            var test3 = new Test
            {
                Name = "Other",
                Difficulty = TestDifficulty.Hard,
                Questions = new List<Question>
                {
                    new Question
                    { Text = "What is the unit of measure of speed in Russia?", Answers = new List<Answer>
                        {
                            new Answer{ Text="kg/sm^3", IsValid = false},
                            new Answer{ Text="m/s", IsValid = true},
                            new Answer{ Text="N/m", IsValid = false},
                            new Answer{ Text="m/s^2", IsValid = false}
                        }, Number = 1
                    },
                    new Question
                    { Text = "How many angles does triangle have?", Answers = new List<Answer>
                        {
                            new Answer{ Text = "One", IsValid = false},
                            new Answer{ Text = "Two", IsValid = false},
                            new Answer{ Text = "Three", IsValid = true}
                        }, Number = 2 
                    }
                }
            };

            var tests = _db.Tests.ToList();
            _db.Tests.Add(test1);
            _db.Tests.Add(test2);
            _db.Tests.Add(test3);
            
            _db.SaveChanges();
        }
        public void SeedRealData() 
        {
            var rdr = new SampleDataReader();
            rdr.SeedTestData();
        }
    }
}
