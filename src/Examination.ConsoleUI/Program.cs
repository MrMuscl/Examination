using Examination.Data.Models;
using Examination.Data.Services;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;

namespace Examination.ConsoleUI
{
    class Program
    {
        static ExaminationContext dbContext = new ExaminationContext();
        static IExaminationData _data = new ExaminationData(dbContext);
        
        static void Main(string[] args)
        {
            GetAllTestItems();
        }

        static void GetAllTestItems() 
        {
            var tts = _data.GetTests();

            var questions = dbContext.Questions.Select(q => q).Include(q => q.Answers).ToList();
            var tests = dbContext.Tests.ToList();
            var testsWithQuestions = dbContext.Tests.Select(t => t).Include(t => t.TestQuestions).ToList();
        }
    }
}
