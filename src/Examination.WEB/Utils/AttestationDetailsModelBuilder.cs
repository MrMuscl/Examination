using Examination.Data.Services;
using Examination.WEB.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Examination.WEB.Utils
{
    public class AttestationDetailsModelBuilder
    {
        public static TestResultsViewMode Build(IExaminationData dataProvider, int attestationId) 
        {
            int correctCount = 0;
            int incorrectCount = 0;
            var attestation = dataProvider.GetAttestationWithQuestionsAndAnswers(attestationId);
            var test = dataProvider.GetTest(attestation.TestId);
            foreach (var protocol in attestation.Protocols)
            {
                if (protocol.Answer.IsValid)
                    correctCount++;
                else
                    incorrectCount++;
            }

            var model = new TestResultsViewMode
            {
                Test = test,
                Attestation = attestation,
                CorrectCount = correctCount,
                IncorrectCount = incorrectCount,
                IsPassed = incorrectCount <= test.ErrorThreshold
            };

            return model;
        } 
    }
}
