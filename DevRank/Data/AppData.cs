using System.Collections.Generic;
using System.Configuration;
using DevRank.Models;
using FakeDb = DevRank.FakeDatabase.FakeDatabase;

namespace DevRank.Data
{
    public static class AppData
    {
        private static bool UseFakeDatabase
        {
            get
            {
                var value = ConfigurationManager.AppSettings["UseFakeDatabase"];
                return string.IsNullOrWhiteSpace(value) || value.ToLower() == "false";
            }
        }

        public static List<ProgrammerProfile> Programmers
        {
            get { return UseFakeDatabase ? FakeDb.Programmers : MySqlDatabase.GetProgrammers(); }
        }

        public static List<Challenge> Challenges
        {
            get { return UseFakeDatabase ? FakeDb.Challenges : MySqlDatabase.GetChallenges(); }
        }

        public static ProgrammerProfile Authenticate(string username, string password)
        {
            return UseFakeDatabase ? FakeDb.Authenticate(username, password) : MySqlDatabase.Authenticate(username, password);
        }

        public static ProgrammerProfile Register(RegisterViewModel model)
        {
            return UseFakeDatabase ? FakeDb.Register(model) : MySqlDatabase.Register(model);
        }

        public static bool UsernameExists(string username)
        {
            return UseFakeDatabase ? FakeDb.UsernameExists(username) : MySqlDatabase.UsernameExists(username);
        }

        public static bool UsernameExistsForAnotherProgrammer(string username, int programmerId)
        {
            return UseFakeDatabase ? FakeDb.UsernameExistsForAnotherProgrammer(username, programmerId) : MySqlDatabase.UsernameExistsForAnotherProgrammer(username, programmerId);
        }

        public static void UpdateProgrammer(ProfileEditViewModel model)
        {
            if (UseFakeDatabase) FakeDb.UpdateProgrammer(model);
            else MySqlDatabase.UpdateProgrammer(model);
        }

        public static ProgrammerProfile GetProgrammer(int id)
        {
            return UseFakeDatabase ? FakeDb.GetProgrammer(id) : MySqlDatabase.GetProgrammer(id);
        }

        public static ProgrammerProfile GetProgrammerByUsername(string username)
        {
            return UseFakeDatabase ? FakeDb.GetProgrammerByUsername(username) : MySqlDatabase.GetProgrammerByUsername(username);
        }

        public static List<ProgrammerProfile> GetTopProgrammers(int count)
        {
            return UseFakeDatabase ? FakeDb.GetTopProgrammers(count) : MySqlDatabase.GetTopProgrammers(count);
        }

        public static Challenge GetChallenge(int id)
        {
            return UseFakeDatabase ? FakeDb.GetChallenge(id) : MySqlDatabase.GetChallenge(id);
        }

        public static int CreateManualChallenges(IEnumerable<Challenge> challenges)
        {
            return UseFakeDatabase ? FakeDb.CreateManualChallenges(challenges) : MySqlDatabase.CreateManualChallenges(challenges);
        }

        public static List<Challenge> GetRecommendedChallenges(int rating, int count)
        {
            return UseFakeDatabase ? FakeDb.GetRecommendedChallenges(rating, count) : MySqlDatabase.GetRecommendedChallenges(rating, count);
        }

        public static List<MatchHistory> GetHistoryForProgrammer(int programmerId)
        {
            return UseFakeDatabase ? FakeDb.GetHistoryForProgrammer(programmerId) : MySqlDatabase.GetHistoryForProgrammer(programmerId);
        }

        public static MatchHistory SaveMatch(MatchHistory match)
        {
            return UseFakeDatabase ? FakeDb.SaveMatch(match) : MySqlDatabase.SaveMatch(match);
        }

        public static CommunityHubViewModel GetCommunityHub(ProgrammerProfile programmer)
        {
            return UseFakeDatabase ? FakeDb.GetCommunityHub(programmer) : MySqlDatabase.GetCommunityHub(programmer);
        }

        public static string ValidateCommunityChallenge(CommunityCreateChallengeViewModel model, ProgrammerProfile programmer)
        {
            return UseFakeDatabase ? FakeDb.ValidateCommunityChallenge(model, programmer) : MySqlDatabase.ValidateCommunityChallenge(model, programmer);
        }

        public static CommunityChallenge SubmitCommunityChallenge(CommunityCreateChallengeViewModel model, ProgrammerProfile programmer)
        {
            return UseFakeDatabase ? FakeDb.SubmitCommunityChallenge(model, programmer) : MySqlDatabase.SubmitCommunityChallenge(model, programmer);
        }

        public static void ModerateCommunityChallenge(int id, string decision, ProgrammerProfile reviewer)
        {
            if (UseFakeDatabase) FakeDb.ModerateCommunityChallenge(id, decision, reviewer);
            else MySqlDatabase.ModerateCommunityChallenge(id, decision, reviewer);
        }
    }
}
