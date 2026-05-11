using System.Collections.Generic;

namespace DevRank.Models
{
    public class DashboardViewModel
    {
        public ProgrammerProfile Programmer { get; set; }
        public IList<MatchHistory> RecentHistory { get; set; }
        public IList<Challenge> RecommendedChallenges { get; set; }
        public IList<ProgrammerProfile> TopProgrammers { get; set; }
        public int CompletedModules { get; set; }
        public int TotalModules { get; set; }
        public string NextInterviewPrompt { get; set; }
        public string CoachInsight { get; set; }
    }
}
