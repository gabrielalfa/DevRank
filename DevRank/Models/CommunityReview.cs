using System;

namespace DevRank.Models
{
    public class CommunityReview
    {
        public int Id { get; set; }
        public int ChallengeId { get; set; }
        public string ReviewerName { get; set; }
        public string Decision { get; set; }
        public string Comment { get; set; }
        public int ReputationDelta { get; set; }
        public DateTime Date { get; set; }
    }
}
