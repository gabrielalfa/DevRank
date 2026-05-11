namespace DevRank.Models
{
    public class CommunityLevel
    {
        public int Level { get; set; }
        public string Name { get; set; }
        public int MinimumReputation { get; set; }
        public string[] Permissions { get; set; }
    }
}
