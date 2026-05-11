namespace DevRank.Models
{
    public class ProgrammerProfile
    {
        public int Id { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string MainStack { get; set; }
        public string Bio { get; set; }
        public int EloRating { get; set; }
        public int Wins { get; set; }
        public int Losses { get; set; }
        public string Level { get; set; }
        public string[] Badges { get; set; }

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
