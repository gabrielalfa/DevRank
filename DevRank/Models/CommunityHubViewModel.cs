using System.Collections.Generic;

namespace DevRank.Models
{
    public class CommunityHubViewModel
    {
        public ProgrammerProfile CurrentProgrammer { get; set; }
        public int Reputation { get; set; }
        public CommunityLevel CurrentLevel { get; set; }
        public CommunityLevel NextLevel { get; set; }
        public int CreatedChallenges { get; set; }
        public int ApprovedChallenges { get; set; }
        public int RejectedChallenges { get; set; }
        public int PendingReviews { get; set; }
        public int HelpfulReviews { get; set; }
        public IList<CommunityChallenge> RecentChallenges { get; set; }
        public IList<CommunityChallenge> ReviewQueue { get; set; }
        public IList<CommunityReview> RecentReviews { get; set; }
        public IList<ProgrammerProfile> CommunityLeaderboard { get; set; }
        public IList<CommunityLevel> Levels { get; set; }
        public AiEvaluationPolicy AiPolicy { get; set; }
        public IList<CommunityFundingSignal> FundingSignals { get; set; }
        public string[] CommunityBadges { get; set; }
    }
}
