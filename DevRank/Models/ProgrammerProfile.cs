namespace DevRank.Models
{
    using System.Collections.Generic;

    public class ProgrammerProfile
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string FakePhotoUrl { get; set; }
        public string MainStack { get; set; }
        public string SecondaryStack { get; set; }
        public string PerceivedSeniority { get; set; }
        public string Bio { get; set; }
        public string ExperienceTime { get; set; }
        public string FakeGitHub { get; set; }
        public string FakeLinkedIn { get; set; }
        public string FakePortfolio { get; set; }
        public string[] Languages { get; set; }
        public int EloRating { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public int WinStreak { get; set; }
        public int CommunityReputation { get; set; }
        public int CommunityLevel { get; set; }
        public int HelpfulReviews { get; set; }
        public string Season { get; set; }
        public string Level { get; set; }
        public string[] Badges { get; set; }
        public IList<TechElo> TechnologyElos { get; set; }
        public IList<SkillScore> Skills { get; set; }
        public IList<IntegrationLink> Integrations { get; set; }
        public IList<EvolutionPoint> Evolution { get; set; }

        public int TotalMatches
        {
            get { return Wins + Losses; }
        }

        public int WinRate
        {
            get
            {
                if (TotalMatches == 0)
                {
                    return 0;
                }

                return (Wins * 100) / TotalMatches;
            }
        }
    }
}
