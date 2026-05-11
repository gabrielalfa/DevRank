namespace DevRank.Models
{
    public class Challenge
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public string Difficulty { get; set; }
        public string Category { get; set; }
        public int MinimumRating { get; set; }
        public string EstimatedTime { get; set; }
        public string FakeCode { get; set; }
        public int ChallengeRating { get; set; }
    }
}
