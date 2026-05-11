namespace DevRank.Services
{
    public class EloService
    {
        public string GetRank(int rating)
        {
            if (rating >= 2200) return "Diamante";
            if (rating >= 1850) return "Platina";
            if (rating >= 1550) return "Ouro";
            if (rating >= 1250) return "Prata";
            return "Bronze";
        }

        public int CalculateVictory(int currentRating, int challengeRating)
        {
            var expectedScore = GetExpectedScore(currentRating, challengeRating);
            return CalculateNewRating(currentRating, expectedScore, 1);
        }

        public int CalculateDefeat(int currentRating, int challengeRating)
        {
            var expectedScore = GetExpectedScore(currentRating, challengeRating);
            return CalculateNewRating(currentRating, expectedScore, 0);
        }

        public int CalculateDelta(int oldRating, int newRating)
        {
            return newRating - oldRating;
        }

        private static double GetExpectedScore(int playerRating, int challengeRating)
        {
            return 1.0 / (1.0 + System.Math.Pow(10, (challengeRating - playerRating) / 400.0));
        }

        private static int CalculateNewRating(int currentRating, double expectedScore, int actualScore)
        {
            const int kFactor = 32;
            return currentRating + (int)System.Math.Round(kFactor * (actualScore - expectedScore));
        }
    }
}
