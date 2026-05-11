using System;

namespace DevRank.Models
{
    public class MatchHistory
    {
        public int Id { get; set; }
        public int ProgrammerId { get; set; }
        public int ChallengeId { get; set; }
        public string ChallengeTitle { get; set; }
        public bool Approved { get; set; }
        public int Score { get; set; }
        public int EloBefore { get; set; }
        public int EloAfter { get; set; }
        public int EloDelta { get; set; }
        public string Feedback { get; set; }
        public DateTime Date { get; set; }
    }
}
