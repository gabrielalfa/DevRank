using System.Collections.Generic;

namespace DevRank.Models
{
    public class ProfileViewModel
    {
        public ProgrammerProfile Programmer { get; set; }
        public IList<MatchHistory> History { get; set; }
    }
}
