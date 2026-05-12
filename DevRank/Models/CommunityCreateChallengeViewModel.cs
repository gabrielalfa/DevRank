namespace DevRank.Models
{
    public class CommunityCreateChallengeViewModel
    {
        public string Title { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Scenario { get; set; }
        public string CodeSnippet { get; set; }
        public string ExpectedAnswer { get; set; }
        public string ErrorMessage { get; set; }
        public string SuccessMessage { get; set; }
        public int DailyLimitRemaining { get; set; }
        public int MinimumReputationRequired { get; set; }
    }
}
