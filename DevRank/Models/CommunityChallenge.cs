using System;

namespace DevRank.Models
{
    public class CommunityChallenge
    {
        public int Id { get; set; }
        public int AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string Title { get; set; }
        public string Type { get; set; }
        public string Category { get; set; }
        public string Scenario { get; set; }
        public string ExpectedAnswer { get; set; }
        public string Status { get; set; }
        public int QualityScore { get; set; }
        public int ClarityScore { get; set; }
        public int RelevanceScore { get; set; }
        public int DifficultyScore { get; set; }
        public int ApprovalRate { get; set; }
        public int AbandonRate { get; set; }
        public int TechnicalLevel { get; set; }
        public int Reports { get; set; }
        public DateTime CreatedAt { get; set; }
        public string ModeratorNote { get; set; }
    }
}
