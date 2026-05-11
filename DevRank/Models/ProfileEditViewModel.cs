namespace DevRank.Models
{
    public class ProfileEditViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Username { get; set; }
        public string Bio { get; set; }
        public string MainStack { get; set; }
        public string SecondaryStack { get; set; }
        public string ExperienceTime { get; set; }
        public string FakeLinkedIn { get; set; }
        public string FakeGitHub { get; set; }
        public string FakePortfolio { get; set; }
        public string LanguagesText { get; set; }
        public string SoftSkillsText { get; set; }
        public string AvatarPreview { get; set; }
        public string ErrorMessage { get; set; }
    }
}
