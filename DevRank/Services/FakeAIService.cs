using System;
using DevRank.Models;

namespace DevRank.Services
{
    public class FakeAIService
    {
        private static readonly string[] PositiveFeedback =
        {
            "Boa separação de responsabilidades e solução fácil de revisar.",
            "A solução reduziu acoplamento e deixou o fluxo mais previsível.",
            "A proposta melhorou performance sem criar complexidade desnecessária.",
            "Bom raciocínio de engenharia, com trade-offs claros."
        };

        private static readonly string[] NegativeFeedback =
        {
            "Detectamos acoplamento excessivo entre camadas.",
            "A solução ainda deixa risco de concorrência em cenários paralelos.",
            "Faltou tratar caminho de erro e comportamento limite.",
            "A abordagem resolve parte do problema, mas piora manutenção."
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
