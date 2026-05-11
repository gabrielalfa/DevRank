using System;
using DevRank.Models;

namespace DevRank.Services
{
    public class FakeAIService
    {
        private static readonly string[] PositiveFeedback =
        {
            "Boa separacao de responsabilidades e solucao facil de revisar.",
            "A solucao reduziu acoplamento e deixou o fluxo mais previsivel.",
            "A proposta melhorou performance sem criar complexidade desnecessaria.",
            "Bom raciocinio de engenharia, com trade-offs claros."
        };

        private static readonly string[] NegativeFeedback =
        {
            "Detectamos acoplamento excessivo entre camadas.",
            "A solucao ainda deixa risco de concorrencia em cenarios paralelos.",
            "Faltou tratar caminho de erro e comportamento limite.",
            "A abordagem resolve parte do problema, mas piora manutencao."
        };

        public MatchHistory Evaluate(ProgrammerProfile programmer, Challenge challenge, string solution)
        {
            var normalizedSolution = solution ?? string.Empty;
            var seed = programmer.Id * 37 + challenge.Id * 97 + normalizedSolution.Length * 11 + DateTime.Now.Second;
            var random = new Random(seed);
            var baseScore = random.Next(45, 96);

            if (normalizedSolution.Length > 180)
            {
                baseScore += 8;
            }

            if (normalizedSolution.IndexOf("service", StringComparison.OrdinalIgnoreCase) >= 0 ||
                normalizedSolution.IndexOf("responsabilidade", StringComparison.OrdinalIgnoreCase) >= 0 ||
                normalizedSolution.IndexOf("cache", StringComparison.OrdinalIgnoreCase) >= 0)
            {
                baseScore += 6;
            }

            var score = Math.Min(baseScore, 100);
            var approved = score >= 70;
            var feedbackList = approved ? PositiveFeedback : NegativeFeedback;
            var feedback = feedbackList[random.Next(feedbackList.Length)];

            return new MatchHistory
            {
                ProgrammerId = programmer.Id,
                ChallengeId = challenge.Id,
                ChallengeTitle = challenge.Title,
                Approved = approved,
                Score = score,
                Feedback = feedback,
                Date = DateTime.Now
            };
        }
    }
}
