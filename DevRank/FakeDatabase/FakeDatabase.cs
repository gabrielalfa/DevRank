using System.Collections.Generic;
using System.Linq;
using System;
using DevRank.Models;
using DevRank.Services;

namespace DevRank.FakeDatabase
{
    public static class FakeDatabase
    {
        private static readonly EloService EloService = new EloService();

        private static int _nextProgrammerId = 6;
        private static int _nextMatchId = 1;
        private static int _nextCommunityChallengeId = 7;
        private static int _nextCommunityReviewId = 4;

        public static readonly List<ProgrammerProfile> Programmers = new List<ProgrammerProfile>
        {
            new ProgrammerProfile
            {
                Id = 1,
                Username = "ana",
                Password = "123",
                Name = "Ana Byte",
                FakePhotoUrl = "avatar://ana-byte",
                MainStack = "ASP.NET MVC",
                SecondaryStack = "Arquitetura / SQL",
                PerceivedSeniority = "Senior",
                Bio = "Especialista em refatoração de sistemas legados.",
                ExperienceTime = "8 anos",
                FakeGitHub = "github.com/anabyte",
                FakeLinkedIn = "linkedin.com/in/anabyte",
                FakePortfolio = "anabyte.dev",
                Languages = new[] { "Português", "Inglês técnico" },
                EloRating = 2310,
                Wins = 48,
                Losses = 12,
                WinStreak = 7,
                CommunityReputation = 1840,
                CommunityLevel = 4,
                HelpfulReviews = 88,
                Season = "Season 01",
                Badges = new[] { "Legacy Slayer", "MVC Elite", "Clean Fix" }
            },
            new ProgrammerProfile
            {
                Id = 2,
                Username = "bruno",
                Password = "123",
                Name = "Bruno Stack",
                FakePhotoUrl = "avatar://bruno-stack",
                MainStack = "C# / SQL",
                SecondaryStack = "Performance",
                PerceivedSeniority = "Pleno+",
                Bio = "Resolve gargalos antes do café esfriar.",
                ExperienceTime = "6 anos",
                FakeGitHub = "github.com/brunostack",
                FakeLinkedIn = "linkedin.com/in/brunostack",
                FakePortfolio = "brunostack.dev",
                Languages = new[] { "Português" },
                EloRating = 1975,
                Wins = 34,
                Losses = 15,
                WinStreak = 4,
                CommunityReputation = 1320,
                CommunityLevel = 3,
                HelpfulReviews = 52,
                Season = "Season 01",
                Badges = new[] { "Perf Hunter", "Query Tamer" }
            },
            new ProgrammerProfile
            {
                Id = 3,
                Username = "carla",
                Password = "123",
                Name = "Carla Thread",
                FakePhotoUrl = "avatar://carla-thread",
                MainStack = "Backend",
                SecondaryStack = "Concorrência / Incidentes",
                PerceivedSeniority = "Senior",
                Bio = "Boa em concorrência, filas e bugs intermitentes.",
                ExperienceTime = "7 anos",
                FakeGitHub = "github.com/carlathread",
                FakeLinkedIn = "linkedin.com/in/carlathread",
                FakePortfolio = "carlathread.io",
                Languages = new[] { "Português", "Inglês" },
                EloRating = 1740,
                Wins = 28,
                Losses = 14,
                WinStreak = 2,
                CommunityReputation = 980,
                CommunityLevel = 3,
                HelpfulReviews = 37,
                Season = "Season 01",
                Badges = new[] { "Race Detector", "Service Mind" }
            },
            new ProgrammerProfile
            {
                Id = 4,
                Username = "diego",
                Password = "123",
                Name = "Diego Razor",
                FakePhotoUrl = "avatar://diego-razor",
                MainStack = "Razor / JavaScript",
                SecondaryStack = "UX / Frontend",
                PerceivedSeniority = "Pleno",
                Bio = "Transforma tela quebrada em fluxo usável.",
                ExperienceTime = "4 anos",
                FakeGitHub = "github.com/diegorazor",
                FakeLinkedIn = "linkedin.com/in/diegorazor",
                FakePortfolio = "diegorazor.dev",
                Languages = new[] { "Português", "Espanhol" },
                EloRating = 1510,
                Wins = 19,
                Losses = 11,
                WinStreak = 3,
                CommunityReputation = 520,
                CommunityLevel = 2,
                HelpfulReviews = 18,
                Season = "Season 01",
                Badges = new[] { "UI Fixer", "Vanilla JS" }
            },
            new ProgrammerProfile
            {
                Id = 5,
                Username = "eva",
                Password = "123",
                Name = "Eva Commit",
                FakePhotoUrl = "avatar://eva-commit",
                MainStack = "Full Stack",
                SecondaryStack = "Produto / Comunicação",
                PerceivedSeniority = "Junior+",
                Bio = "Entrega soluções simples, testáveis e diretas.",
                ExperienceTime = "2 anos",
                FakeGitHub = "github.com/evacommit",
                FakeLinkedIn = "linkedin.com/in/evacommit",
                FakePortfolio = "evacommit.dev",
                Languages = new[] { "Português" },
                EloRating = 1320,
                Wins = 14,
                Losses = 13,
                WinStreak = 1,
                CommunityReputation = 210,
                CommunityLevel = 1,
                HelpfulReviews = 9,
                Season = "Season 01",
                Badges = new[] { "Fast Ship", "Bug Basher" }
            }
        };

        public static readonly List<Challenge> Challenges = new List<Challenge>
        {
            new Challenge
            {
                Id = 1,
                Title = "Refatorar endpoint MVC",
                Description = "Um endpoint mistura regra de negócio, validação e acesso a dados. Proponha uma refatoração simples para melhorar manutenção.",
                Difficulty = "Médio",
                Category = "ASP.NET MVC",
                MinimumRating = 900,
                EstimatedTime = "20 min",
                ChallengeRating = 1350,
                FakeCode = "public ActionResult Save(Order order)\r\n{\r\n    if (order.Total <= 0) return View(order);\r\n    var db = new AppDbContext();\r\n    db.Orders.Add(order);\r\n    db.SaveChanges();\r\n    SendEmail(order.CustomerEmail);\r\n    return RedirectToAction(\"Index\");\r\n}"
            },
            new Challenge
            {
                Id = 2,
                Title = "Corrigir query lenta",
                Description = "Uma listagem está lenta em horário de pico. Explique como reduzir consultas repetidas e melhorar a leitura dos dados.",
                Difficulty = "Médio",
                Category = "Performance",
                MinimumRating = 1000,
                EstimatedTime = "25 min",
                ChallengeRating = 1450,
                FakeCode = "foreach (var customer in customers)\r\n{\r\n    customer.LastOrder = db.Orders\r\n        .Where(o => o.CustomerId == customer.Id)\r\n        .OrderByDescending(o => o.CreatedAt)\r\n        .FirstOrDefault();\r\n}"
            },
            new Challenge
            {
                Id = 3,
                Title = "Detectar bug de concorrência",
                Description = "Dois usuários podem consumir o mesmo cupom ao mesmo tempo. Identifique o risco e proponha uma correção pragmática.",
                Difficulty = "Difícil",
                Category = "Concorrência",
                MinimumRating = 1300,
                EstimatedTime = "30 min",
                ChallengeRating = 1750,
                FakeCode = "var coupon = repository.Get(code);\r\nif (!coupon.Used)\r\n{\r\n    coupon.Used = true;\r\n    repository.Save(coupon);\r\n    ApplyDiscount(coupon);\r\n}"
            },
            new Challenge
            {
                Id = 4,
                Title = "Melhorar arquitetura de service",
                Description = "Um service concentra validação, persistência, notificação e logs. Quebre responsabilidades sem criar arquitetura exagerada.",
                Difficulty = "Médio",
                Category = "Arquitetura",
                MinimumRating = 1100,
                EstimatedTime = "25 min",
                ChallengeRating = 1500,
                FakeCode = "public class BillingService\r\n{\r\n    public void Charge(Invoice invoice)\r\n    {\r\n        Validate(invoice);\r\n        Save(invoice);\r\n        SendEmail(invoice);\r\n        WriteAudit(invoice);\r\n    }\r\n}"
            },
            new Challenge
            {
                Id = 5,
                Title = "Resolver problema de performance",
                Description = "Uma tela carrega tudo de uma vez e trava o navegador. Proponha paginação, filtros ou carregamento sob demanda.",
                Difficulty = "Fácil",
                Category = "Frontend",
                MinimumRating = 800,
                EstimatedTime = "15 min",
                ChallengeRating = 1200,
                FakeCode = "var rows = GetAllRows();\r\nreturn View(rows);"
            },
            new Challenge
            {
                Id = 6,
                Title = "Revisar código legado",
                Description = "Avalie um trecho legado com nomes ruins, duplicação e regras escondidas. Sugira uma melhoria incremental.",
                Difficulty = "Fácil",
                Category = "Code Review",
                MinimumRating = 800,
                EstimatedTime = "15 min",
                ChallengeRating = 1150,
                FakeCode = "if (x == 1)\r\n{\r\n    total = total - total * 0.1m;\r\n}\r\nelse if (x == 2)\r\n{\r\n    total = total - total * 0.15m;\r\n}"
            }
        };

        public static readonly List<MatchHistory> MatchHistories = new List<MatchHistory>();

        public static readonly List<CommunityLevel> CommunityLevels = new List<CommunityLevel>
        {
            new CommunityLevel { Level = 1, Name = "Contributor", MinimumReputation = 0, Permissions = new[] { "Sugerir desafios", "Comentar", "Reportar problemas" } },
            new CommunityLevel { Level = 2, Name = "Trusted Contributor", MinimumReputation = 350, Permissions = new[] { "Criar desafios públicos", "Avaliar respostas simples", "Sugerir tags" } },
            new CommunityLevel { Level = 3, Name = "Technical Reviewer", MinimumReputation = 850, Permissions = new[] { "Revisar desafios", "Validar respostas", "Aprovar conteúdo" } },
            new CommunityLevel { Level = 4, Name = "Senior Curator", MinimumReputation = 1500, Permissions = new[] { "Editar desafios", "Organizar categorias", "Moderar abusos" } },
            new CommunityLevel { Level = 5, Name = "Arena Architect", MinimumReputation = 2500, Permissions = new[] { "Criar desafios oficiais", "Validar rankings", "Destacar conteúdos" } }
        };

        public static readonly List<CommunityChallenge> CommunityChallenges = new List<CommunityChallenge>
        {
            new CommunityChallenge { Id = 1, AuthorId = 1, AuthorName = "Ana Byte", Title = "Incidente de API com timeout intermitente", Type = "Cenário real", Category = "Debugging", Scenario = "Uma API MVC começa a responder acima de 8s durante pico. O time precisa mitigar, comunicar e investigar sem derrubar produção.", ExpectedAnswer = "Diagnóstico incremental, logs, métricas, rollback controlado e comunicação objetiva.", Status = "Homologado", HomologationStage = "Homologado pela comunidade", RequiredApprovals = 3, ApprovalVotes = 5, RejectionVotes = 0, ConfidenceScore = 94, QualityScore = 94, ClarityScore = 91, RelevanceScore = 96, DifficultyScore = 82, ApprovalRate = 88, AbandonRate = 11, TechnicalLevel = 4, Reports = 0, CreatedAt = DateTime.Now.AddDays(-12), ModeratorNote = "Excelente cenário de produção.", IsOfficialCandidate = true },
            new CommunityChallenge { Id = 2, AuthorId = 2, AuthorName = "Bruno Stack", Title = "Query N+1 em relatório financeiro", Type = "Desafio técnico", Category = "SQL", Scenario = "Relatório executa centenas de consultas por cliente e trava no fechamento mensal.", ExpectedAnswer = "Identificar N+1, projetar consulta agregada, paginar e medir antes/depois.", Status = "Em homologação", HomologationStage = "Community Review", RequiredApprovals = 3, ApprovalVotes = 2, RejectionVotes = 0, ConfidenceScore = 76, QualityScore = 86, ClarityScore = 78, RelevanceScore = 92, DifficultyScore = 74, ApprovalRate = 0, AbandonRate = 0, TechnicalLevel = 3, Reports = 0, CreatedAt = DateTime.Now.AddDays(-2), ModeratorNote = "Precisa validar critérios de aceite.", IsOfficialCandidate = true },
            new CommunityChallenge { Id = 3, AuthorId = 3, AuthorName = "Carla Thread", Title = "Conflito entre dev sênior e produto", Type = "Comportamental", Category = "Liderança", Scenario = "Um dev sênior bloqueia uma decisão de produto em público. Como conduzir sem humilhar ninguém e sem perder prazo?", ExpectedAnswer = "Escuta, alinhamento privado, trade-offs, decisão documentada e restauração de confiança.", Status = "Em homologação", HomologationStage = "Community Review", RequiredApprovals = 3, ApprovalVotes = 1, RejectionVotes = 0, ConfidenceScore = 68, QualityScore = 82, ClarityScore = 85, RelevanceScore = 89, DifficultyScore = 80, ApprovalRate = 0, AbandonRate = 0, TechnicalLevel = 3, Reports = 1, CreatedAt = DateTime.Now.AddDays(-1), ModeratorNote = "Bom potencial para entrevista de liderança.", IsOfficialCandidate = true },
            new CommunityChallenge { Id = 4, AuthorId = 4, AuthorName = "Diego Razor", Title = "Tela lenta com renderização pesada", Type = "Desafio técnico", Category = "Frontend", Scenario = "Dashboard carrega 2.000 linhas, filtros travam e o usuário acha que o sistema caiu.", ExpectedAnswer = "Paginação, virtualização, debounce, estados de loading e medição.", Status = "Devolvido para melhoria", HomologationStage = "Aguardando autor", RequiredApprovals = 3, ApprovalVotes = 0, RejectionVotes = 1, ConfidenceScore = 41, QualityScore = 61, ClarityScore = 58, RelevanceScore = 75, DifficultyScore = 55, ApprovalRate = 0, AbandonRate = 0, TechnicalLevel = 2, Reports = 0, CreatedAt = DateTime.Now.AddDays(-3), ModeratorNote = "Adicionar código base e critérios de aceite.", IsOfficialCandidate = false },
            new CommunityChallenge { Id = 5, AuthorId = 5, AuthorName = "Eva Commit", Title = "Resposta genérica sobre arquitetura limpa", Type = "Entrevista", Category = "Arquitetura", Scenario = "Candidato responde com buzzwords sobre Clean Architecture sem explicar trade-offs.", ExpectedAnswer = "Pressionar por contexto, custo, simplicidade e decisões pragmáticas.", Status = "Rejeitado", HomologationStage = "Reprovado por qualidade", RequiredApprovals = 3, ApprovalVotes = 0, RejectionVotes = 3, ConfidenceScore = 24, QualityScore = 42, ClarityScore = 46, RelevanceScore = 55, DifficultyScore = 35, ApprovalRate = 0, AbandonRate = 0, TechnicalLevel = 1, Reports = 2, CreatedAt = DateTime.Now.AddDays(-4), ModeratorNote = "Muito genérico, precisa de cenário concreto.", IsOfficialCandidate = false },
            new CommunityChallenge { Id = 6, AuthorId = 1, AuthorName = "Ana Byte", Title = "PR com regra crítica escondida no controller", Type = "Review de código", Category = "Legado", Scenario = "Pull request move uma regra fiscal para dentro do controller para 'resolver rápido'.", ExpectedAnswer = "Apontar risco, sugerir extração incremental e proteger comportamento com testes.", Status = "Homologado", HomologationStage = "Homologado pela comunidade", RequiredApprovals = 3, ApprovalVotes = 4, RejectionVotes = 0, ConfidenceScore = 90, QualityScore = 91, ClarityScore = 88, RelevanceScore = 94, DifficultyScore = 70, ApprovalRate = 84, AbandonRate = 13, TechnicalLevel = 3, Reports = 0, CreatedAt = DateTime.Now.AddDays(-9), ModeratorNote = "Ótimo para revisão prática.", IsOfficialCandidate = true }
        };

        public static readonly List<CommunityReview> CommunityReviews = new List<CommunityReview>
        {
            new CommunityReview { Id = 1, ChallengeId = 4, ReviewerName = "Ana Byte", Decision = "Devolvido", Comment = "Cenário bom, mas falta código inicial e métrica de sucesso.", ClarityScore = 58, RelevanceScore = 75, ReviewerReputation = 1840, ReputationDelta = 12, Date = DateTime.Now.AddDays(-2) },
            new CommunityReview { Id = 2, ChallengeId = 5, ReviewerName = "Bruno Stack", Decision = "Rejeitado", Comment = "Muito abstrato. Precisa de conflito real e resposta esperada mais objetiva.", ClarityScore = 46, RelevanceScore = 55, ReviewerReputation = 1320, ReputationDelta = 8, Date = DateTime.Now.AddDays(-3) },
            new CommunityReview { Id = 3, ChallengeId = 2, ReviewerName = "Carla Thread", Decision = "Em análise", Comment = "Bom desafio, aguardando critérios de aceite.", ClarityScore = 78, RelevanceScore = 92, ReviewerReputation = 980, ReputationDelta = 6, Date = DateTime.Now.AddHours(-12) }
        };

        public static List<ProgrammerProfile> GetTopProgrammers(int count)
        {
            ApplyComputedRanks();
            return Programmers
                .OrderByDescending(programmer => programmer.EloRating)
                .Take(count)
                .ToList();
        }

        public static ProgrammerProfile Authenticate(string username, string password)
        {
            return Programmers.FirstOrDefault(programmer =>
                programmer.Username.ToLower() == (username ?? string.Empty).ToLower() &&
                programmer.Password == password);
        }

        public static ProgrammerProfile Register(RegisterViewModel model)
        {
            var programmer = new ProgrammerProfile
            {
                Id = _nextProgrammerId++,
                Username = model.Username,
                Password = model.Password,
                Name = model.Name,
                FakePhotoUrl = "avatar://" + model.Username,
                MainStack = model.MainStack,
                SecondaryStack = "Em descoberta",
                PerceivedSeniority = "Em calibragem",
                Bio = model.Bio,
                ExperienceTime = "Não informado",
                FakeGitHub = "github.com/" + model.Username,
                FakeLinkedIn = "linkedin.com/in/" + model.Username,
                FakePortfolio = model.Username + ".dev",
                Languages = new[] { "Português" },
                EloRating = 1000,
                Wins = 0,
                Losses = 0,
                WinStreak = 0,
                CommunityReputation = 80,
                CommunityLevel = 1,
                HelpfulReviews = 0,
                Season = "Season 01",
                Level = "Bronze",
                Badges = new[] { "Rookie", "Arena Ready" }
            };

            HydrateAdvancedProfile(programmer);
            Programmers.Add(programmer);
            return programmer;
        }

        public static bool UsernameExists(string username)
        {
            return Programmers.Any(programmer => programmer.Username.ToLower() == (username ?? string.Empty).ToLower());
        }

        public static bool UsernameExistsForAnotherProgrammer(string username, int programmerId)
        {
            return Programmers.Any(programmer =>
                programmer.Id != programmerId &&
                programmer.Username.ToLower() == (username ?? string.Empty).ToLower());
        }

        public static void UpdateProgrammer(ProfileEditViewModel model)
        {
            var programmer = Programmers.FirstOrDefault(item => item.Id == model.Id);

            if (programmer == null)
            {
                return;
            }

            programmer.Name = model.Name;
            programmer.Username = model.Username;
            programmer.Bio = model.Bio;
            programmer.MainStack = model.MainStack;
            programmer.SecondaryStack = model.SecondaryStack;
            programmer.ExperienceTime = model.ExperienceTime;
            programmer.FakeLinkedIn = model.FakeLinkedIn;
            programmer.FakeGitHub = model.FakeGitHub;
            programmer.FakePortfolio = model.FakePortfolio;
            programmer.Languages = SplitCsv(model.LanguagesText);

            if (!string.IsNullOrWhiteSpace(model.AvatarPreview))
            {
                programmer.FakePhotoUrl = model.AvatarPreview;
            }

            programmer.Integrations = null;
            HydrateAdvancedProfile(programmer);
        }

        public static ProgrammerProfile GetProgrammer(int id)
        {
            ApplyComputedRanks();
            return Programmers.FirstOrDefault(programmer => programmer.Id == id);
        }

        public static ProgrammerProfile GetProgrammerByUsername(string username)
        {
            ApplyComputedRanks();
            return Programmers.FirstOrDefault(programmer =>
                programmer.Username.ToLower() == (username ?? string.Empty).ToLower());
        }

        public static Challenge GetChallenge(int id)
        {
            return Challenges.FirstOrDefault(challenge => challenge.Id == id);
        }

        public static List<Challenge> GetRecommendedChallenges(int rating, int count)
        {
            return Challenges
                .OrderBy(challenge => System.Math.Abs(challenge.ChallengeRating - rating))
                .Take(count)
                .ToList();
        }

        public static List<MatchHistory> GetHistoryForProgrammer(int programmerId)
        {
            return MatchHistories
                .Where(match => match.ProgrammerId == programmerId)
                .OrderByDescending(match => match.Date)
                .ToList();
        }

        public static MatchHistory SaveMatch(MatchHistory match)
        {
            match.Id = _nextMatchId++;
            MatchHistories.Add(match);
            return match;
        }

        public static CommunityHubViewModel GetCommunityHub(ProgrammerProfile programmer)
        {
            ApplyCommunityLevels();

            var currentLevel = GetCommunityLevel(programmer.CommunityReputation);
            var nextLevel = CommunityLevels.FirstOrDefault(level => level.MinimumReputation > programmer.CommunityReputation);
            var authored = CommunityChallenges.Where(challenge => challenge.AuthorId == programmer.Id).ToList();

            return new CommunityHubViewModel
            {
                CurrentProgrammer = programmer,
                Reputation = programmer.CommunityReputation,
                CurrentLevel = currentLevel,
                NextLevel = nextLevel,
                CreatedChallenges = authored.Count,
                ApprovedChallenges = authored.Count(challenge => challenge.Status == "Aprovado"),
                RejectedChallenges = authored.Count(challenge => challenge.Status == "Rejeitado"),
                PendingReviews = CommunityChallenges.Count(challenge => challenge.Status == "Em homologação"),
                HelpfulReviews = programmer.HelpfulReviews,
                RecentChallenges = CommunityChallenges.OrderByDescending(challenge => challenge.CreatedAt).Take(6).ToList(),
                ReviewQueue = CommunityChallenges.Where(challenge => challenge.Status == "Em homologação").OrderByDescending(challenge => challenge.QualityScore).ToList(),
                RecentReviews = CommunityReviews.OrderByDescending(review => review.Date).Take(5).ToList(),
                CommunityLeaderboard = Programmers.OrderByDescending(item => item.CommunityReputation).Take(10).ToList(),
                Levels = CommunityLevels,
                AiPolicy = GetAiEvaluationPolicy(),
                FundingSignals = GetCommunityFundingSignals(),
                CommunityBadges = new[] { "Bug Hunter", "SQL Master", "Interview Mentor", "Legacy Slayer", "Clean Architect", "Performance Expert", "Community Helper", "Elite Reviewer", "Trusted Engineer" }
            };
        }

        public static string ValidateCommunityChallenge(CommunityCreateChallengeViewModel model, ProgrammerProfile programmer)
        {
            if (programmer.CommunityReputation < 120)
            {
                return "Reputação comunitária insuficiente para publicar. Continue comentando e revisando antes de sugerir desafios.";
            }

            if (string.IsNullOrWhiteSpace(model.Title) || string.IsNullOrWhiteSpace(model.Scenario))
            {
                return "Informe título e cenário real. Contribuições vagas entram em shadow review automaticamente.";
            }

            if (model.Scenario.Trim().Length < 160)
            {
                return "O cenário está curto demais. Descreva contexto, restrição, pressão e critério de avaliação.";
            }

            if (LooksGeneric(model.Scenario) || LooksGeneric(model.ExpectedAnswer))
            {
                return "A contribuição parece genérica. Adicione detalhes concretos, trade-offs e sinais de avaliação.";
            }

            return string.Empty;
        }

        public static CommunityChallenge SubmitCommunityChallenge(CommunityCreateChallengeViewModel model, ProgrammerProfile programmer)
        {
            var quality = CalculateQualityScore(model);
            var challenge = new CommunityChallenge
            {
                Id = _nextCommunityChallengeId++,
                AuthorId = programmer.Id,
                AuthorName = programmer.Name,
                Title = model.Title,
                Type = model.Type,
                Category = model.Category,
                Scenario = model.Scenario,
                ExpectedAnswer = model.ExpectedAnswer,
                Status = "Em homologação",
                HomologationStage = "Community Review",
                RequiredApprovals = 3,
                ApprovalVotes = 0,
                RejectionVotes = 0,
                ConfidenceScore = Math.Max(35, quality - 12),
                QualityScore = quality,
                ClarityScore = Math.Min(96, quality - 3),
                RelevanceScore = Math.Min(98, quality + 5),
                DifficultyScore = 72,
                ApprovalRate = 0,
                AbandonRate = 0,
                TechnicalLevel = 3,
                Reports = 0,
                CreatedAt = DateTime.Now,
                ModeratorNote = "Shadow review automático: aguardando validação comunitária.",
                IsOfficialCandidate = quality >= 75
            };

            CommunityChallenges.Add(challenge);
            programmer.CommunityReputation += 15;
            ApplyCommunityLevels();

            return challenge;
        }

        public static void ModerateCommunityChallenge(int id, string decision, ProgrammerProfile reviewer)
        {
            var challenge = CommunityChallenges.FirstOrDefault(item => item.Id == id);

            if (challenge == null || reviewer == null)
            {
                return;
            }

            var normalizedDecision = string.IsNullOrWhiteSpace(decision) ? "Devolvido para melhoria" : decision;
            ApplyCommunityVote(challenge, normalizedDecision);
            challenge.ModeratorNote = "Voto comunitário aplicado por " + reviewer.Name + ".";

            var author = Programmers.FirstOrDefault(item => item.Id == challenge.AuthorId);
            if (author != null)
            {
                if (normalizedDecision == "Aprovado")
                {
                    author.CommunityReputation += 80;
                }
                else if (normalizedDecision == "Rejeitado")
                {
                    author.CommunityReputation = Math.Max(0, author.CommunityReputation - 35);
                }
                else
                {
                    author.CommunityReputation += 5;
                }
            }

            reviewer.HelpfulReviews += 1;
            reviewer.CommunityReputation += 12;

            CommunityReviews.Add(new CommunityReview
            {
                Id = _nextCommunityReviewId++,
                ChallengeId = id,
                ReviewerName = reviewer.Name,
                Decision = normalizedDecision,
                Comment = challenge.ModeratorNote,
                ClarityScore = challenge.ClarityScore,
                RelevanceScore = challenge.RelevanceScore,
                ReviewerReputation = reviewer.CommunityReputation,
                ReputationDelta = 12,
                Date = DateTime.Now
            });

            ApplyCommunityLevels();
        }

        private static void ApplyComputedRanks()
        {
            foreach (var programmer in Programmers)
            {
                programmer.Level = EloService.GetRank(programmer.EloRating);
                HydrateAdvancedProfile(programmer);
            }

            ApplyCommunityLevels();
        }

        private static void ApplyCommunityLevels()
        {
            foreach (var programmer in Programmers)
            {
                programmer.CommunityLevel = GetCommunityLevel(programmer.CommunityReputation).Level;
            }
        }

        private static CommunityLevel GetCommunityLevel(int reputation)
        {
            return CommunityLevels
                .Where(level => level.MinimumReputation <= reputation)
                .OrderByDescending(level => level.MinimumReputation)
                .First();
        }

        private static int CalculateQualityScore(CommunityCreateChallengeViewModel model)
        {
            var score = 55;
            score += Math.Min(20, (model.Scenario ?? string.Empty).Length / 35);
            score += Math.Min(15, (model.ExpectedAnswer ?? string.Empty).Length / 30);
            score += string.IsNullOrWhiteSpace(model.Category) ? 0 : 8;
            return Math.Min(96, score);
        }

        private static bool LooksGeneric(string value)
        {
            var text = (value ?? string.Empty).ToLower();
            return text.Contains("boas práticas") && text.Contains("melhorar") && text.Length < 240;
        }

        private static void ApplyCommunityVote(CommunityChallenge challenge, string decision)
        {
            if (decision == "Aprovado")
            {
                challenge.ApprovalVotes += 1;
            }
            else if (decision == "Rejeitado")
            {
                challenge.RejectionVotes += 1;
            }
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
                return;
            }

            if (challenge.RejectionVotes >= 2)
            {
                challenge.Status = "Rejeitado";
                challenge.HomologationStage = "Reprovado por qualidade";
                challenge.IsOfficialCandidate = false;
                return;
            }

            challenge.Status = "Em homologação";
            challenge.HomologationStage = "Community Review";
        }

        private static AiEvaluationPolicy GetAiEvaluationPolicy()
        {
            return new AiEvaluationPolicy
            {
                IsEnabled = false,
                Mode = "Heurística local + homologação comunitária",
                MonthlyTokenBudget = 0,
                TokensUsed = 0,
                EstimatedCostInCents = 0,
                FundingStatus = "Aguardando tração e contribuição da comunidade",
                Strategy = "Preparado para IA assistida no futuro, sem consumo de tokens no protótipo atual."
            };
        }

        private static List<CommunityFundingSignal> GetCommunityFundingSignals()
        {
            return new List<CommunityFundingSignal>
            {
                new CommunityFundingSignal { Name = "Planos premium", Description = "Devs pagam por análises avançadas, histórico exportável e simulados ilimitados.", Status = "Futuro", Confidence = 68 },
                new CommunityFundingSignal { Name = "Empresas observadoras", Description = "Times pagam para acompanhar rankings, perfis públicos e sinais de evolução.", Status = "Futuro", Confidence = 74 },
                new CommunityFundingSignal { Name = "Créditos de avaliação", Description = "Usuários compram créditos para avaliações com IA quando o custo fizer sentido.", Status = "Futuro", Confidence = 61 },
                new CommunityFundingSignal { Name = "Challenges patrocinados", Description = "Empresas patrocinam trilhas técnicas e ajudam a financiar tokens.", Status = "Futuro", Confidence = 57 }
            };
        }

        private static void HydrateAdvancedProfile(ProgrammerProfile programmer)
        {
            programmer.Skills = BuildSkills(programmer.EloRating);
            programmer.TechnologyElos = BuildTechElos(programmer);

            if (programmer.Integrations == null)
            {
                programmer.Integrations = BuildIntegrations(programmer);
            }

            programmer.Evolution = BuildEvolution(programmer.EloRating);
        }

        private static List<SkillScore> BuildSkills(int rating)
        {
            var baseScore = System.Math.Max(45, System.Math.Min(92, rating / 28));

            return new List<SkillScore>
            {
                new SkillScore { Name = "Comunicação", Score = System.Math.Min(96, baseScore - 2), Group = "Humana" },
                new SkillScore { Name = "Liderança", Score = System.Math.Min(96, baseScore - 8), Group = "Humana" },
                new SkillScore { Name = "Arquitetura", Score = System.Math.Min(96, baseScore + 4), Group = "Técnica" },
                new SkillScore { Name = "Backend", Score = System.Math.Min(96, baseScore + 8), Group = "Técnica" },
                new SkillScore { Name = "Frontend", Score = System.Math.Min(96, baseScore - 5), Group = "Técnica" },
                new SkillScore { Name = "Banco de dados", Score = System.Math.Min(96, baseScore + 2), Group = "Técnica" },
                new SkillScore { Name = "DevOps", Score = System.Math.Min(96, baseScore - 10), Group = "Técnica" },
                new SkillScore { Name = "Resolução de problemas", Score = System.Math.Min(98, baseScore + 10), Group = "Prática" },
                new SkillScore { Name = "Inteligência emocional", Score = System.Math.Min(94, baseScore - 4), Group = "Humana" },
                new SkillScore { Name = "Trabalho em equipe", Score = System.Math.Min(95, baseScore + 1), Group = "Humana" },
                new SkillScore { Name = "Performance sob pressão", Score = System.Math.Min(96, baseScore - 1), Group = "Prática" }
            };
        }

        private static List<TechElo> BuildTechElos(ProgrammerProfile programmer)
        {
            return new List<TechElo>
            {
                new TechElo { Technology = programmer.MainStack, Rating = programmer.EloRating, Rank = EloService.GetRank(programmer.EloRating) },
                new TechElo { Technology = ".NET", Rating = programmer.EloRating - 80, Rank = EloService.GetRank(programmer.EloRating - 80) },
                new TechElo { Technology = "JavaScript", Rating = programmer.EloRating - 180, Rank = EloService.GetRank(programmer.EloRating - 180) },
                new TechElo { Technology = "Arquitetura", Rating = programmer.EloRating - 40, Rank = EloService.GetRank(programmer.EloRating - 40) },
                new TechElo { Technology = "Comunicação", Rating = programmer.EloRating - 130, Rank = EloService.GetRank(programmer.EloRating - 130) }
            };
        }

        private static List<IntegrationLink> BuildIntegrations(ProgrammerProfile programmer)
        {
            return new List<IntegrationLink>
            {
                new IntegrationLink { Name = "GitHub", Url = programmer.FakeGitHub, Status = "Simulado", Description = "Futuro: importar repos e sinais de contribuição." },
                new IntegrationLink { Name = "LinkedIn", Url = programmer.FakeLinkedIn, Status = "Simulado", Description = "Futuro: comparar narrativa profissional com performance real." },
                new IntegrationLink { Name = "Portfólio", Url = programmer.FakePortfolio, Status = "Simulado", Description = "Futuro: anexar projetos, cases e evidências." },
                new IntegrationLink { Name = "Currículo", Url = "currículo-" + programmer.Username + ".pdf", Status = "Pendente", Description = "Futuro: gerar relatório técnico exportável." }
            };
        }

        private static List<EvolutionPoint> BuildEvolution(int rating)
        {
            return new List<EvolutionPoint>
            {
                new EvolutionPoint { Label = "Semana 1", Elo = rating - 210, Confidence = 52 },
                new EvolutionPoint { Label = "Semana 2", Elo = rating - 160, Confidence = 58 },
                new EvolutionPoint { Label = "Semana 3", Elo = rating - 95, Confidence = 65 },
                new EvolutionPoint { Label = "Semana 4", Elo = rating - 40, Confidence = 72 },
                new EvolutionPoint { Label = "Hoje", Elo = rating, Confidence = 78 }
            };
        }

        private static string[] SplitCsv(string value)
        {
            if (string.IsNullOrWhiteSpace(value))
            {
                return new[] { "Português" };
            }

            return value
                .Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries)
                .Select(item => item.Trim())
                .Where(item => item.Length > 0)
                .ToArray();
        }
    }
}
