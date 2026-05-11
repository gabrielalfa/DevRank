using System.Collections.Generic;

namespace DevRank.Models
{
    public class HomeViewModel
    {
        public IList<ProgrammerProfile> TopProgrammers { get; set; }
        public int TotalDevelopers { get; set; }
        public int TotalChallenges { get; set; }
        public int TotalMatches { get; set; }
    }
}
