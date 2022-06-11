using AdventureWorksSample;
using Examination.Data.Models;
using Examination.Data.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Examination.ConsoleUI
{
    class Program
    {
        static ExaminationContext dbContext = new ExaminationContext();
        static IExaminationData _data = new ExaminationData(dbContext);
        static AdventureWorks2014Context _ADdbContext = new AdventureWorks2014Context();


        private static void InitDb()
        {
            var initDB = new InitDatabase(dbContext);

            initDB.EnsureDbDeleted();
            initDB.EnsureDbCreated();
            initDB.SeedTestData();
        }
        
        static void Main(string[] args)
        {
            //GetAllTestItems();
            //AddTest();
            //GetAllQuestionsForTest(1);
            //GetQuestion(1);
            //Adwentureworks_GetProj();
            //AddQuestion();

            
            InitDb();

        }
        
        static void GetAllTestItems() 
        {
            var tts = _data.GetTests();

            var questions = dbContext.Questions.Select(q => q).Include(q => q.Answers).ToList();
            var tests = dbContext.Tests.ToList();
            //var testsWithQuestions = dbContext.Tests.Select(t => t).Include(t => t.TestQuestions).ToList();
        }

        static void AddTest() 
        {
            
            var test = new Test();
            test.Difficulty = TestDifficulty.Easy;
            test.Name = "UI test2";

            dbContext.Tests.Add(test);
            dbContext.SaveChanges();
        }

        static IEnumerable<string> GetAllQuestionsForTest(int id) 
        {
            var test = dbContext.Tests.Where(t => t.Id == id).Single();
            
            //var Tests = dbContext.Tests()
            
            //var questions = dbContext.Questions.Join(dbContext.TestQuestions,
            //                                    q => q.Id,
            //                                    t => t.QuestionId,
            //                                    (q, t) =>
            //                                    new { QuestionText = q.Text, TestId = t.TestId }).ToList();

            //var res = new List<string>();
            //foreach (var itm in questions)
            //    res.Add(itm.QuestionText);

            return null;
        }

        static void GetQuestion(int id) 
        {
            var questions = dbContext.Questions.Include(q=>q.Answers).ToList();

            var a = questions[0].Answers.ToList()[0];

            //var test = dbContext.TestQuestions.Include(tq => tq.Question).Where(tq => tq.TestId == 1).Select(//tq => tq.Question).ToList();


        }

        static void Adwentureworks_GetProj() 
        {
            var products = (from p in _ADdbContext.Products
                            where p.Name.StartsWith("A") & p.ProductModelId != null
                            select p
                   )
                   .Take(5)
                   .ToList();

            foreach (var p in products)
            {
                Console.ReadLine();
                Console.WriteLine("{0} {1} {2}", p.ProductId, p.Name, p.ProductModel.Name);
            }
        }

        static void AddQuestion() 
        {
            var test = dbContext.Tests.Where(t => t.Id == 1).FirstOrDefault();
            var question = new Question { Text = "New question1"};
            test.Questions.Add(question);
            
            dbContext.SaveChanges();
        }
    }

}
