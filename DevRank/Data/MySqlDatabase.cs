using System;
using System.Collections.Generic;
using System.Linq;
using DevRank.Models;
using DevRank.Services;
using MySql.Data.MySqlClient;

namespace DevRank.Data
{
    public static class MySqlDatabase
    {
        private static readonly EloService EloService = new EloService();

        public static List<ProgrammerProfile> GetProgrammers()
        {
            var programmers = new List<ProgrammerProfile>();

            using (var connection = DbConnectionFactory.Create())
            {
                connection.Open();
                using (var command = new MySqlCommand("SELECT * FROM programmers ORDER BY elo_rating DESC", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        programmers.Add(MapProgrammer(reader));
                    }
                }
            }

            return programmers;
        }

        public static ProgrammerProfile Authenticate(string username, string password)
        {
            using (var connection = DbConnectionFactory.Create())
            {
                connection.Open();
                using (var command = new MySqlCommand("SELECT * FROM programmers WHERE username = @username AND password = @password LIMIT 1", connection))
                {
                    command.Parameters.AddWithValue("@username", username ?? string.Empty);
                    command.Parameters.AddWithValue("@password", password ?? string.Empty);

                    using (var reader = command.ExecuteReader())
                    {
                        return reader.Read() ? MapProgrammer(reader) : null;
                    }
                }
            }
        }

        public static ProgrammerProfile Register(RegisterViewModel model)
        {
            using (var connection = DbConnectionFactory.Create())
            {
                connection.Open();
                const string sql = @"
INSERT INTO programmers
(username, password, name, fake_photo_url, main_stack, secondary_stack, perceived_seniority, bio, experience_time, fake_github, fake_linkedin, fake_portfolio, languages_text, elo_rating, wins, losses, win_streak, community_reputation, community_level, helpful_reviews, season, level_name, badges_text)
VALUES
(@username, @password, @name, @fake_photo_url, @main_stack, @secondary_stack, @perceived_seniority, @bio, @experience_time, @fake_github, @fake_linkedin, @fake_portfolio, @languages_text, 1000, 0, 0, 0, 80, 1, 0, 'Season 01', 'Bronze', 'Rookie, Arena Ready');
SELECT LAST_INSERT_ID();";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@username", model.Username);
                    command.Parameters.AddWithValue("@password", model.Password);
                    command.Parameters.AddWithValue("@name", model.Name);
                    command.Parameters.AddWithValue("@fake_photo_url", "avatar://" + model.Username);
                    command.Parameters.AddWithValue("@main_stack", model.MainStack);
                    command.Parameters.AddWithValue("@secondary_stack", "Em descoberta");
                    command.Parameters.AddWithValue("@perceived_seniority", "Em calibragem");
                    command.Parameters.AddWithValue("@bio", model.Bio);
                    command.Parameters.AddWithValue("@experience_time", "Não informado");
                    command.Parameters.AddWithValue("@fake_github", "github.com/" + model.Username);
                    command.Parameters.AddWithValue("@fake_linkedin", "linkedin.com/in/" + model.Username);
                    command.Parameters.AddWithValue("@fake_portfolio", model.Username + ".dev");
                    command.Parameters.AddWithValue("@languages_text", "Português");

                    var id = Convert.ToInt32(command.ExecuteScalar());
                    return GetProgrammer(id);
                }
            }
        }

        public static bool UsernameExists(string username)
        {
            return UsernameCount(username, null) > 0;
        }

        public static bool UsernameExistsForAnotherProgrammer(string username, int programmerId)
        {
            return UsernameCount(username, programmerId) > 0;
        }

        public static void UpdateProgrammer(ProfileEditViewModel model)
        {
            using (var connection = DbConnectionFactory.Create())
            {
                connection.Open();
                const string sql = @"
UPDATE programmers SET
    username = @username,
    name = @name,
    bio = @bio,
    main_stack = @main_stack,
    secondary_stack = @secondary_stack,
    experience_time = @experience_time,
    fake_github = @fake_github,
    fake_linkedin = @fake_linkedin,
    fake_portfolio = @fake_portfolio,
    languages_text = @languages_text,
    fake_photo_url = @fake_photo_url,
    updated_at = CURRENT_TIMESTAMP
WHERE id = @id";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@id", model.Id);
                    command.Parameters.AddWithValue("@username", model.Username);
                    command.Parameters.AddWithValue("@name", model.Name);
                    command.Parameters.AddWithValue("@bio", model.Bio);
                    command.Parameters.AddWithValue("@main_stack", model.MainStack);
                    command.Parameters.AddWithValue("@secondary_stack", model.SecondaryStack);
                    command.Parameters.AddWithValue("@experience_time", model.ExperienceTime);
                    command.Parameters.AddWithValue("@fake_github", model.FakeGitHub);
                    command.Parameters.AddWithValue("@fake_linkedin", model.FakeLinkedIn);
                    command.Parameters.AddWithValue("@fake_portfolio", model.FakePortfolio);
                    command.Parameters.AddWithValue("@languages_text", model.LanguagesText);
                    command.Parameters.AddWithValue("@fake_photo_url", string.IsNullOrWhiteSpace(model.AvatarPreview) ? "avatar://" + model.Username : model.AvatarPreview);
                    command.ExecuteNonQuery();
                }
            }
        }

        public static ProgrammerProfile GetProgrammer(int id)
        {
            using (var connection = DbConnectionFactory.Create())
            {
                connection.Open();
                using (var command = new MySqlCommand("SELECT * FROM programmers WHERE id = @id LIMIT 1", connection))
                {
                    command.Parameters.AddWithValue("@id", id);
                    using (var reader = command.ExecuteReader())
                    {
                        return reader.Read() ? MapProgrammer(reader) : null;
                    }
                }
            }
        }

        public static ProgrammerProfile GetProgrammerByUsername(string username)
        {
            using (var connection = DbConnectionFactory.Create())
            {
                connection.Open();
                using (var command = new MySqlCommand("SELECT * FROM programmers WHERE username = @username LIMIT 1", connection))
                {
                    command.Parameters.AddWithValue("@username", username ?? string.Empty);
                    using (var reader = command.ExecuteReader())
                    {
                        return reader.Read() ? MapProgrammer(reader) : null;
                    }
                }
            }
        }

        public static List<ProgrammerProfile> GetTopProgrammers(int count)
        {
            return GetProgrammers().Take(count).ToList();
        }

        public static List<Challenge> GetChallenges()
        {
            var challenges = new List<Challenge>();

            using (var connection = DbConnectionFactory.Create())
            {
                connection.Open();
                using (var command = new MySqlCommand("SELECT * FROM challenges WHERE is_active = 1 ORDER BY minimum_rating", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        challenges.Add(MapChallenge(reader));
                    }
                }
            }

            return challenges;
        }

        public static Challenge GetChallenge(int id)
        {
            return GetChallenges().FirstOrDefault(challenge => challenge.Id == id);
        }

        public static List<Challenge> GetRecommendedChallenges(int rating, int count)
        {
            return GetChallenges()
                .OrderBy(challenge => Math.Abs(challenge.ChallengeRating - rating))
                .Take(count)
                .ToList();
        }

        public static List<MatchHistory> GetHistoryForProgrammer(int programmerId)
        {
            var history = new List<MatchHistory>();

            using (var connection = DbConnectionFactory.Create())
            {
                connection.Open();
                using (var command = new MySqlCommand("SELECT * FROM match_histories WHERE programmer_id = @programmer_id ORDER BY date DESC", connection))
                {
                    command.Parameters.AddWithValue("@programmer_id", programmerId);
                    using (var reader = command.ExecuteReader())
                    {
                        while (reader.Read())
                        {
                            history.Add(MapMatch(reader));
                        }
                    }
                }
            }

            return history;
        }

        public static MatchHistory SaveMatch(MatchHistory match)
        {
            using (var connection = DbConnectionFactory.Create())
            {
                connection.Open();
                const string insertSql = @"
INSERT INTO match_histories
(programmer_id, challenge_id, challenge_title, approved, score, elo_before, elo_after, elo_delta, feedback, date)
VALUES
(@programmer_id, @challenge_id, @challenge_title, @approved, @score, @elo_before, @elo_after, @elo_delta, @feedback, @date);
SELECT LAST_INSERT_ID();";

                using (var command = new MySqlCommand(insertSql, connection))
                {
                    command.Parameters.AddWithValue("@programmer_id", match.ProgrammerId);
                    command.Parameters.AddWithValue("@challenge_id", match.ChallengeId);
                    command.Parameters.AddWithValue("@challenge_title", match.ChallengeTitle);
                    command.Parameters.AddWithValue("@approved", match.Approved ? 1 : 0);
                    command.Parameters.AddWithValue("@score", match.Score);
                    command.Parameters.AddWithValue("@elo_before", match.EloBefore);
                    command.Parameters.AddWithValue("@elo_after", match.EloAfter);
                    command.Parameters.AddWithValue("@elo_delta", match.EloDelta);
                    command.Parameters.AddWithValue("@feedback", match.Feedback);
                    command.Parameters.AddWithValue("@date", match.Date);
                    match.Id = Convert.ToInt32(command.ExecuteScalar());
                }

                const string updateSql = @"
UPDATE programmers SET
    elo_rating = @elo,
    level_name = @level,
    wins = wins + @win,
    losses = losses + @loss,
    win_streak = CASE WHEN @win = 1 THEN win_streak + 1 ELSE 0 END
WHERE id = @id";

                using (var command = new MySqlCommand(updateSql, connection))
                {
                    command.Parameters.AddWithValue("@elo", match.EloAfter);
                    command.Parameters.AddWithValue("@level", EloService.GetRank(match.EloAfter));
                    command.Parameters.AddWithValue("@win", match.Approved ? 1 : 0);
                    command.Parameters.AddWithValue("@loss", match.Approved ? 0 : 1);
                    command.Parameters.AddWithValue("@id", match.ProgrammerId);
                    command.ExecuteNonQuery();
                }
            }

            return match;
        }

        public static CommunityHubViewModel GetCommunityHub(ProgrammerProfile programmer)
        {
            var challenges = GetCommunityChallenges();
            var authored = challenges.Where(challenge => challenge.AuthorId == programmer.Id).ToList();

            return new CommunityHubViewModel
            {
                CurrentProgrammer = programmer,
                Reputation = programmer.CommunityReputation,
                CurrentLevel = GetCommunityLevel(programmer.CommunityReputation),
                NextLevel = GetCommunityLevels().FirstOrDefault(level => level.MinimumReputation > programmer.CommunityReputation),
                CreatedChallenges = authored.Count,
                ApprovedChallenges = authored.Count(challenge => challenge.Status == "Homologado"),
                RejectedChallenges = authored.Count(challenge => challenge.Status == "Rejeitado"),
                PendingReviews = challenges.Count(challenge => challenge.Status == "Em homologação"),
                HelpfulReviews = programmer.HelpfulReviews,
                RecentChallenges = challenges.OrderByDescending(challenge => challenge.CreatedAt).Take(6).ToList(),
                ReviewQueue = challenges.Where(challenge => challenge.Status == "Em homologação").OrderByDescending(challenge => challenge.QualityScore).ToList(),
                RecentReviews = GetCommunityReviews().OrderByDescending(review => review.Date).Take(5).ToList(),
                CommunityLeaderboard = GetProgrammers().OrderByDescending(item => item.CommunityReputation).Take(10).ToList(),
                Levels = GetCommunityLevels(),
                AiPolicy = GetAiEvaluationPolicy(),
                FundingSignals = GetCommunityFundingSignals(),
                CommunityBadges = new[] { "Bug Hunter", "SQL Master", "Interview Mentor", "Legacy Slayer", "Clean Architect", "Performance Expert", "Community Helper", "Elite Reviewer", "Trusted Engineer" }
            };
        }

        public static string ValidateCommunityChallenge(CommunityCreateChallengeViewModel model, ProgrammerProfile programmer)
        {
            if (programmer.CommunityReputation < 120) return "Reputação comunitária insuficiente para publicar.";
            if (string.IsNullOrWhiteSpace(model.Title) || string.IsNullOrWhiteSpace(model.Scenario)) return "Informe título e cenário real.";
            if (model.Scenario.Trim().Length < 160) return "O cenário está curto demais. Descreva contexto, restrição, pressão e critério de avaliação.";
            return string.Empty;
        }

        public static CommunityChallenge SubmitCommunityChallenge(CommunityCreateChallengeViewModel model, ProgrammerProfile programmer)
        {
            var quality = Math.Min(96, 55 + ((model.Scenario ?? string.Empty).Length / 35) + ((model.ExpectedAnswer ?? string.Empty).Length / 30));

            using (var connection = DbConnectionFactory.Create())
            {
                connection.Open();
                const string sql = @"
INSERT INTO community_challenges
(author_id, author_name, title, challenge_type, category, scenario, expected_answer, status, homologation_stage, required_approvals, confidence_score, quality_score, clarity_score, relevance_score, difficulty_score, technical_level, moderator_note, is_official_candidate)
VALUES
(@author_id, @author_name, @title, @challenge_type, @category, @scenario, @expected_answer, 'Em homologação', 'Community Review', 3, @confidence_score, @quality_score, @clarity_score, @relevance_score, 72, 3, 'Shadow review automático: aguardando validação comunitária.', @official);
SELECT LAST_INSERT_ID();";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@author_id", programmer.Id);
                    command.Parameters.AddWithValue("@author_name", programmer.Name);
                    command.Parameters.AddWithValue("@title", model.Title);
                    command.Parameters.AddWithValue("@challenge_type", model.Type);
                    command.Parameters.AddWithValue("@category", model.Category);
                    command.Parameters.AddWithValue("@scenario", model.Scenario);
                    command.Parameters.AddWithValue("@expected_answer", model.ExpectedAnswer);
                    command.Parameters.AddWithValue("@confidence_score", Math.Max(35, quality - 12));
                    command.Parameters.AddWithValue("@quality_score", quality);
                    command.Parameters.AddWithValue("@clarity_score", Math.Max(0, quality - 3));
                    command.Parameters.AddWithValue("@relevance_score", Math.Min(98, quality + 5));
                    command.Parameters.AddWithValue("@official", quality >= 75 ? 1 : 0);
                    var id = Convert.ToInt32(command.ExecuteScalar());

                    IncrementCommunityReputation(connection, programmer.Id, 15, 0);
                    return GetCommunityChallenges().First(challenge => challenge.Id == id);
                }
            }
        }

        public static void ModerateCommunityChallenge(int id, string decision, ProgrammerProfile reviewer)
        {
            var challenge = GetCommunityChallenges().FirstOrDefault(item => item.Id == id);
            if (challenge == null || reviewer == null) return;

            ApplyCommunityVote(challenge, decision);

            using (var connection = DbConnectionFactory.Create())
            {
                connection.Open();
                const string updateSql = @"
UPDATE community_challenges SET
    status = @status,
    homologation_stage = @stage,
    approval_votes = @approval_votes,
    rejection_votes = @rejection_votes,
    confidence_score = @confidence_score,
    is_official_candidate = @official,
    moderator_note = @note,
    updated_at = CURRENT_TIMESTAMP
WHERE id = @id";

                using (var command = new MySqlCommand(updateSql, connection))
                {
                    command.Parameters.AddWithValue("@id", challenge.Id);
                    command.Parameters.AddWithValue("@status", challenge.Status);
                    command.Parameters.AddWithValue("@stage", challenge.HomologationStage);
                    command.Parameters.AddWithValue("@approval_votes", challenge.ApprovalVotes);
                    command.Parameters.AddWithValue("@rejection_votes", challenge.RejectionVotes);
                    command.Parameters.AddWithValue("@confidence_score", challenge.ConfidenceScore);
                    command.Parameters.AddWithValue("@official", challenge.IsOfficialCandidate ? 1 : 0);
                    command.Parameters.AddWithValue("@note", "Voto comunitário aplicado por " + reviewer.Name + ".");
                    command.ExecuteNonQuery();
                }

                const string insertReview = @"
INSERT INTO community_reviews
(challenge_id, reviewer_name, decision, comment, clarity_score, relevance_score, reviewer_reputation, reputation_delta, date)
VALUES
(@challenge_id, @reviewer_name, @decision, @comment, @clarity_score, @relevance_score, @reviewer_reputation, 12, CURRENT_TIMESTAMP)";

                using (var command = new MySqlCommand(insertReview, connection))
                {
                    command.Parameters.AddWithValue("@challenge_id", challenge.Id);
                    command.Parameters.AddWithValue("@reviewer_name", reviewer.Name);
                    command.Parameters.AddWithValue("@decision", decision);
                    command.Parameters.AddWithValue("@comment", "Revisão comunitária registrada.");
                    command.Parameters.AddWithValue("@clarity_score", challenge.ClarityScore);
                    command.Parameters.AddWithValue("@relevance_score", challenge.RelevanceScore);
                    command.Parameters.AddWithValue("@reviewer_reputation", reviewer.CommunityReputation);
                    command.ExecuteNonQuery();
                }

                IncrementCommunityReputation(connection, reviewer.Id, 12, 1);
                if (challenge.Status == "Homologado") IncrementCommunityReputation(connection, challenge.AuthorId, 80, 0);
                if (challenge.Status == "Rejeitado") IncrementCommunityReputation(connection, challenge.AuthorId, -35, 0);
            }
        }

        private static int UsernameCount(string username, int? exceptId)
        {
            using (var connection = DbConnectionFactory.Create())
            {
                connection.Open();
                var sql = "SELECT COUNT(*) FROM programmers WHERE username = @username";
                if (exceptId.HasValue) sql += " AND id <> @id";

                using (var command = new MySqlCommand(sql, connection))
                {
                    command.Parameters.AddWithValue("@username", username ?? string.Empty);
                    if (exceptId.HasValue) command.Parameters.AddWithValue("@id", exceptId.Value);
                    return Convert.ToInt32(command.ExecuteScalar());
                }
            }
        }

        private static List<CommunityChallenge> GetCommunityChallenges()
        {
            var challenges = new List<CommunityChallenge>();
            using (var connection = DbConnectionFactory.Create())
            {
                connection.Open();
                using (var command = new MySqlCommand("SELECT * FROM community_challenges ORDER BY created_at DESC", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read()) challenges.Add(MapCommunityChallenge(reader));
                }
            }
            return challenges;
        }

        private static List<CommunityReview> GetCommunityReviews()
        {
            var reviews = new List<CommunityReview>();
            using (var connection = DbConnectionFactory.Create())
            {
                connection.Open();
                using (var command = new MySqlCommand("SELECT * FROM community_reviews ORDER BY date DESC", connection))
                using (var reader = command.ExecuteReader())
                {
                    while (reader.Read())
                    {
                        reviews.Add(new CommunityReview
                        {
                            Id = ReadInt(reader, "id"),
                            ChallengeId = ReadInt(reader, "challenge_id"),
                            ReviewerName = ReadString(reader, "reviewer_name"),
                            Decision = ReadString(reader, "decision"),
                            Comment = ReadString(reader, "comment"),
                            ClarityScore = ReadInt(reader, "clarity_score"),
                            RelevanceScore = ReadInt(reader, "relevance_score"),
                            ReviewerReputation = ReadInt(reader, "reviewer_reputation"),
                            ReputationDelta = ReadInt(reader, "reputation_delta"),
                            Date = ReadDate(reader, "date")
                        });
                    }
                }
            }
            return reviews;
        }

        private static void IncrementCommunityReputation(MySqlConnection connection, int programmerId, int reputationDelta, int helpfulDelta)
        {
            using (var command = new MySqlCommand("UPDATE programmers SET community_reputation = GREATEST(0, community_reputation + @rep), helpful_reviews = helpful_reviews + @helpful WHERE id = @id", connection))
            {
                command.Parameters.AddWithValue("@rep", reputationDelta);
                command.Parameters.AddWithValue("@helpful", helpfulDelta);
                command.Parameters.AddWithValue("@id", programmerId);
                command.ExecuteNonQuery();
            }
        }

        private static ProgrammerProfile MapProgrammer(MySqlDataReader reader)
        {
            var rating = ReadInt(reader, "elo_rating");
            var programmer = new ProgrammerProfile
            {
                Id = ReadInt(reader, "id"),
                Username = ReadString(reader, "username"),
                Password = ReadString(reader, "password"),
                Name = ReadString(reader, "name"),
                FakePhotoUrl = ReadString(reader, "fake_photo_url"),
                MainStack = ReadString(reader, "main_stack"),
                SecondaryStack = ReadString(reader, "secondary_stack"),
                PerceivedSeniority = ReadString(reader, "perceived_seniority"),
                Bio = ReadString(reader, "bio"),
                ExperienceTime = ReadString(reader, "experience_time"),
                FakeGitHub = ReadString(reader, "fake_github"),
                FakeLinkedIn = ReadString(reader, "fake_linkedin"),
                FakePortfolio = ReadString(reader, "fake_portfolio"),
                Languages = SplitCsv(ReadString(reader, "languages_text")),
                EloRating = rating,
                Wins = ReadInt(reader, "wins"),
                Losses = ReadInt(reader, "losses"),
                WinStreak = ReadInt(reader, "win_streak"),
                CommunityReputation = ReadInt(reader, "community_reputation"),
                CommunityLevel = ReadInt(reader, "community_level"),
                HelpfulReviews = ReadInt(reader, "helpful_reviews"),
                Season = ReadString(reader, "season"),
                Level = EloService.GetRank(rating),
                Badges = SplitCsv(ReadString(reader, "badges_text"))
            };

            HydrateAdvancedProfile(programmer);
            return programmer;
        }

        private static Challenge MapChallenge(MySqlDataReader reader)
        {
            return new Challenge
            {
                Id = ReadInt(reader, "id"),
                Title = ReadString(reader, "title"),
                Description = ReadString(reader, "description"),
                Difficulty = ReadString(reader, "difficulty"),
                Category = ReadString(reader, "category"),
                MinimumRating = ReadInt(reader, "minimum_rating"),
                EstimatedTime = ReadString(reader, "estimated_time"),
                FakeCode = ReadString(reader, "fake_code"),
                ChallengeRating = ReadInt(reader, "challenge_rating")
            };
        }

        private static MatchHistory MapMatch(MySqlDataReader reader)
        {
            return new MatchHistory
            {
                Id = ReadInt(reader, "id"),
                ProgrammerId = ReadInt(reader, "programmer_id"),
                ChallengeId = ReadInt(reader, "challenge_id"),
                ChallengeTitle = ReadString(reader, "challenge_title"),
                Approved = ReadInt(reader, "approved") == 1,
                Score = ReadInt(reader, "score"),
                EloBefore = ReadInt(reader, "elo_before"),
                EloAfter = ReadInt(reader, "elo_after"),
                EloDelta = ReadInt(reader, "elo_delta"),
                Feedback = ReadString(reader, "feedback"),
                Date = ReadDate(reader, "date")
            };
        }

        private static CommunityChallenge MapCommunityChallenge(MySqlDataReader reader)
        {
            return new CommunityChallenge
            {
                Id = ReadInt(reader, "id"),
                AuthorId = ReadInt(reader, "author_id"),
                AuthorName = ReadString(reader, "author_name"),
                Title = ReadString(reader, "title"),
                Type = ReadString(reader, "challenge_type"),
                Category = ReadString(reader, "category"),
                Scenario = ReadString(reader, "scenario"),
                ExpectedAnswer = ReadString(reader, "expected_answer"),
                Status = ReadString(reader, "status"),
                HomologationStage = ReadString(reader, "homologation_stage"),
                RequiredApprovals = ReadInt(reader, "required_approvals"),
                ApprovalVotes = ReadInt(reader, "approval_votes"),
                RejectionVotes = ReadInt(reader, "rejection_votes"),
                ConfidenceScore = ReadInt(reader, "confidence_score"),
                QualityScore = ReadInt(reader, "quality_score"),
                ClarityScore = ReadInt(reader, "clarity_score"),
                RelevanceScore = ReadInt(reader, "relevance_score"),
                DifficultyScore = ReadInt(reader, "difficulty_score"),
                ApprovalRate = ReadInt(reader, "approval_rate"),
                AbandonRate = ReadInt(reader, "abandon_rate"),
                TechnicalLevel = ReadInt(reader, "technical_level"),
                Reports = ReadInt(reader, "reports"),
                ModeratorNote = ReadString(reader, "moderator_note"),
                IsOfficialCandidate = ReadInt(reader, "is_official_candidate") == 1,
                CreatedAt = ReadDate(reader, "created_at")
            };
        }

        private static void HydrateAdvancedProfile(ProgrammerProfile programmer)
        {
            var baseScore = Math.Max(45, Math.Min(92, programmer.EloRating / 28));
            programmer.Skills = new List<SkillScore>
            {
                new SkillScore { Name = "Comunicação", Score = Math.Min(96, baseScore - 2), Group = "Humana" },
                new SkillScore { Name = "Liderança", Score = Math.Min(96, baseScore - 8), Group = "Humana" },
                new SkillScore { Name = "Arquitetura", Score = Math.Min(96, baseScore + 4), Group = "Técnica" },
                new SkillScore { Name = "Backend", Score = Math.Min(96, baseScore + 8), Group = "Técnica" },
                new SkillScore { Name = "Frontend", Score = Math.Min(96, baseScore - 5), Group = "Técnica" },
                new SkillScore { Name = "Banco de dados", Score = Math.Min(96, baseScore + 2), Group = "Técnica" },
                new SkillScore { Name = "DevOps", Score = Math.Min(96, baseScore - 10), Group = "Técnica" },
                new SkillScore { Name = "Resolução de problemas", Score = Math.Min(98, baseScore + 10), Group = "Prática" }
            };
            programmer.TechnologyElos = new List<TechElo>
            {
                new TechElo { Technology = programmer.MainStack, Rating = programmer.EloRating, Rank = EloService.GetRank(programmer.EloRating) },
                new TechElo { Technology = ".NET", Rating = programmer.EloRating - 80, Rank = EloService.GetRank(programmer.EloRating - 80) },
                new TechElo { Technology = "Arquitetura", Rating = programmer.EloRating - 40, Rank = EloService.GetRank(programmer.EloRating - 40) },
                new TechElo { Technology = "Comunicação", Rating = programmer.EloRating - 130, Rank = EloService.GetRank(programmer.EloRating - 130) }
            };
            programmer.Integrations = new List<IntegrationLink>
            {
                new IntegrationLink { Name = "GitHub", Url = programmer.FakeGitHub, Status = "Simulado", Description = "Futuro: importar repos e sinais de contribuição." },
                new IntegrationLink { Name = "LinkedIn", Url = programmer.FakeLinkedIn, Status = "Simulado", Description = "Futuro: comparar narrativa profissional com performance real." },
                new IntegrationLink { Name = "Portfólio", Url = programmer.FakePortfolio, Status = "Simulado", Description = "Futuro: anexar projetos, cases e evidências." }
            };
            programmer.Evolution = new List<EvolutionPoint>
            {
                new EvolutionPoint { Label = "Semana 1", Elo = programmer.EloRating - 210, Confidence = 52 },
                new EvolutionPoint { Label = "Semana 2", Elo = programmer.EloRating - 160, Confidence = 58 },
                new EvolutionPoint { Label = "Semana 3", Elo = programmer.EloRating - 95, Confidence = 65 },
                new EvolutionPoint { Label = "Hoje", Elo = programmer.EloRating, Confidence = 78 }
            };
        }

        private static void ApplyCommunityVote(CommunityChallenge challenge, string decision)
        {
            if (decision == "Aprovado") challenge.ApprovalVotes += 1;
            else if (decision == "Rejeitado") challenge.RejectionVotes += 1;
            else
            {
                challenge.Status = "Devolvido para melhoria";
                challenge.HomologationStage = "Aguardando autor";
                challenge.ConfidenceScore = Math.Max(20, challenge.ConfidenceScore - 10);
                return;
            }

            challenge.ConfidenceScore = Math.Min(99, challenge.QualityScore + (challenge.ApprovalVotes * 6) - (challenge.RejectionVotes * 12));
            if (challenge.ApprovalVotes >= challenge.RequiredApprovals && challenge.ConfidenceScore >= 78)
            {
                challenge.Status = "Homologado";
                challenge.HomologationStage = "Homologado pela comunidade";
                challenge.IsOfficialCandidate = true;
            }
            else if (challenge.RejectionVotes >= 2)
            {
                challenge.Status = "Rejeitado";
                challenge.HomologationStage = "Reprovado por qualidade";
                challenge.IsOfficialCandidate = false;
            }
            else
            {
                challenge.Status = "Em homologação";
                challenge.HomologationStage = "Community Review";
            }
        }

        private static List<CommunityLevel> GetCommunityLevels()
        {
            return new List<CommunityLevel>
            {
                new CommunityLevel { Level = 1, Name = "Contributor", MinimumReputation = 0, Permissions = new[] { "Sugerir desafios", "Comentar" } },
                new CommunityLevel { Level = 2, Name = "Trusted Contributor", MinimumReputation = 350, Permissions = new[] { "Criar desafios públicos" } },
                new CommunityLevel { Level = 3, Name = "Technical Reviewer", MinimumReputation = 850, Permissions = new[] { "Revisar desafios" } },
                new CommunityLevel { Level = 4, Name = "Senior Curator", MinimumReputation = 1500, Permissions = new[] { "Moderar abusos" } },
                new CommunityLevel { Level = 5, Name = "Arena Architect", MinimumReputation = 2500, Permissions = new[] { "Criar desafios oficiais" } }
            };
        }

        private static CommunityLevel GetCommunityLevel(int reputation)
        {
            return GetCommunityLevels().Where(level => level.MinimumReputation <= reputation).OrderByDescending(level => level.MinimumReputation).First();
        }

        private static AiEvaluationPolicy GetAiEvaluationPolicy()
        {
            return new AiEvaluationPolicy { IsEnabled = false, Mode = "Heurística local + homologação comunitária", MonthlyTokenBudget = 0, TokensUsed = 0, EstimatedCostInCents = 0, FundingStatus = "Aguardando tração e contribuição da comunidade", Strategy = "Preparado para IA assistida no futuro, sem consumo de tokens no protótipo atual." };
        }

        private static List<CommunityFundingSignal> GetCommunityFundingSignals()
        {
            return new List<CommunityFundingSignal>
            {
                new CommunityFundingSignal { Name = "Planos premium", Description = "Devs pagam por análises avançadas.", Status = "Futuro", Confidence = 68 },
                new CommunityFundingSignal { Name = "Empresas observadoras", Description = "Times acompanham rankings e perfis públicos.", Status = "Futuro", Confidence = 74 },
                new CommunityFundingSignal { Name = "Créditos de avaliação", Description = "Usuários compram créditos para avaliações com IA.", Status = "Futuro", Confidence = 61 }
            };
        }

        private static string ReadString(MySqlDataReader reader, string name)
        {
            var value = reader[name];
            return value == DBNull.Value ? string.Empty : Convert.ToString(value);
        }

        private static int ReadInt(MySqlDataReader reader, string name)
        {
            var value = reader[name];
            return value == DBNull.Value ? 0 : Convert.ToInt32(value);
        }

        private static DateTime ReadDate(MySqlDataReader reader, string name)
        {
            var value = reader[name];
            return value == DBNull.Value ? DateTime.Now : Convert.ToDateTime(value);
        }

        private static string[] SplitCsv(string value)
        {
            if (string.IsNullOrWhiteSpace(value)) return new string[0];
            return value.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries).Select(item => item.Trim()).ToArray();
        }
    }
}
