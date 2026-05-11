using System.Collections.Generic;
using System.Linq;
using DevRank.Models;
using DevRank.Services;

namespace DevRank.FakeDatabase
{
    public static class FakeDatabase
    {
        private static readonly EloService EloService = new EloService();

        private static int _nextProgrammerId = 6;
        private static int _nextMatchId = 1;

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
                Bio = "Especialista em refatoracao de sistemas legados.",
                ExperienceTime = "8 anos",
                FakeGitHub = "github.com/anabyte",
                FakeLinkedIn = "linkedin.com/in/anabyte",
                FakePortfolio = "anabyte.dev",
                Languages = new[] { "Portugues", "Ingles tecnico" },
                EloRating = 2310,
                Wins = 48,
                Losses = 12,
                WinStreak = 7,
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
                Bio = "Resolve gargalos antes do cafe esfriar.",
                ExperienceTime = "6 anos",
                FakeGitHub = "github.com/brunostack",
                FakeLinkedIn = "linkedin.com/in/brunostack",
                FakePortfolio = "brunostack.dev",
                Languages = new[] { "Portugues" },
                EloRating = 1975,
                Wins = 34,
                Losses = 15,
                WinStreak = 4,
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
                SecondaryStack = "Concorrencia / Incidentes",
                PerceivedSeniority = "Senior",
                Bio = "Boa em concorrencia, filas e bugs intermitentes.",
                ExperienceTime = "7 anos",
                FakeGitHub = "github.com/carlathread",
                FakeLinkedIn = "linkedin.com/in/carlathread",
                FakePortfolio = "carlathread.io",
                Languages = new[] { "Portugues", "Ingles" },
                EloRating = 1740,
                Wins = 28,
                Losses = 14,
                WinStreak = 2,
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
                Bio = "Transforma tela quebrada em fluxo usavel.",
                ExperienceTime = "4 anos",
                FakeGitHub = "github.com/diegorazor",
                FakeLinkedIn = "linkedin.com/in/diegorazor",
                FakePortfolio = "diegorazor.dev",
                Languages = new[] { "Portugues", "Espanhol" },
                EloRating = 1510,
                Wins = 19,
                Losses = 11,
                WinStreak = 3,
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
                SecondaryStack = "Produto / Comunicacao",
                PerceivedSeniority = "Junior+",
                Bio = "Entrega solucoes simples, testaveis e diretas.",
                ExperienceTime = "2 anos",
                FakeGitHub = "github.com/evacommit",
                FakeLinkedIn = "linkedin.com/in/evacommit",
                FakePortfolio = "evacommit.dev",
                Languages = new[] { "Portugues" },
                EloRating = 1320,
                Wins = 14,
                Losses = 13,
                WinStreak = 1,
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
                Description = "Um endpoint mistura regra de negocio, validacao e acesso a dados. Proponha uma refatoracao simples para melhorar manutencao.",
                Difficulty = "Medio",
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
                Description = "Uma listagem esta lenta em horario de pico. Explique como reduzir consultas repetidas e melhorar a leitura dos dados.",
                Difficulty = "Medio",
                Category = "Performance",
                MinimumRating = 1000,
                EstimatedTime = "25 min",
                ChallengeRating = 1450,
                FakeCode = "foreach (var customer in customers)\r\n{\r\n    customer.LastOrder = db.Orders\r\n        .Where(o => o.CustomerId == customer.Id)\r\n        .OrderByDescending(o => o.CreatedAt)\r\n        .FirstOrDefault();\r\n}"
            },
            new Challenge
            {
                Id = 3,
                Title = "Detectar bug de concorrencia",
                Description = "Dois usuarios podem consumir o mesmo cupom ao mesmo tempo. Identifique o risco e proponha uma correcao pragmatica.",
                Difficulty = "Dificil",
                Category = "Concorrencia",
                MinimumRating = 1300,
                EstimatedTime = "30 min",
                ChallengeRating = 1750,
                FakeCode = "var coupon = repository.Get(code);\r\nif (!coupon.Used)\r\n{\r\n    coupon.Used = true;\r\n    repository.Save(coupon);\r\n    ApplyDiscount(coupon);\r\n}"
            },
            new Challenge
            {
                Id = 4,
                Title = "Melhorar arquitetura de service",
                Description = "Um service concentra validacao, persistencia, notificacao e logs. Quebre responsabilidades sem criar arquitetura exagerada.",
                Difficulty = "Medio",
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
                Description = "Uma tela carrega tudo de uma vez e trava o navegador. Proponha paginacao, filtros ou carregamento sob demanda.",
                Difficulty = "Facil",
                Category = "Frontend",
                MinimumRating = 800,
                EstimatedTime = "15 min",
                ChallengeRating = 1200,
                FakeCode = "var rows = GetAllRows();\r\nreturn View(rows);"
            },
            new Challenge
            {
                Id = 6,
                Title = "Revisar codigo legado",
                Description = "Avalie um trecho legado com nomes ruins, duplicacao e regras escondidas. Sugira uma melhoria incremental.",
                Difficulty = "Facil",
                Category = "Code Review",
                MinimumRating = 800,
                EstimatedTime = "15 min",
                ChallengeRating = 1150,
                FakeCode = "if (x == 1)\r\n{\r\n    total = total - total * 0.1m;\r\n}\r\nelse if (x == 2)\r\n{\r\n    total = total - total * 0.15m;\r\n}"
            }
        };

        public static readonly List<MatchHistory> MatchHistories = new List<MatchHistory>();

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
                ExperienceTime = "Nao informado",
                FakeGitHub = "github.com/" + model.Username,
                FakeLinkedIn = "linkedin.com/in/" + model.Username,
                FakePortfolio = model.Username + ".dev",
                Languages = new[] { "Portugues" },
                EloRating = 1000,
                Wins = 0,
                Losses = 0,
                WinStreak = 0,
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

        public static ProgrammerProfile GetProgrammer(int id)
        {
            ApplyComputedRanks();
            return Programmers.FirstOrDefault(programmer => programmer.Id == id);
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

        private static void ApplyComputedRanks()
        {
            foreach (var programmer in Programmers)
            {
                programmer.Level = EloService.GetRank(programmer.EloRating);
                HydrateAdvancedProfile(programmer);
            }
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
                new SkillScore { Name = "Comunicacao", Score = System.Math.Min(96, baseScore - 2), Group = "Humana" },
                new SkillScore { Name = "Lideranca", Score = System.Math.Min(96, baseScore - 8), Group = "Humana" },
                new SkillScore { Name = "Arquitetura", Score = System.Math.Min(96, baseScore + 4), Group = "Tecnica" },
                new SkillScore { Name = "Backend", Score = System.Math.Min(96, baseScore + 8), Group = "Tecnica" },
                new SkillScore { Name = "Frontend", Score = System.Math.Min(96, baseScore - 5), Group = "Tecnica" },
                new SkillScore { Name = "Banco de dados", Score = System.Math.Min(96, baseScore + 2), Group = "Tecnica" },
                new SkillScore { Name = "DevOps", Score = System.Math.Min(96, baseScore - 10), Group = "Tecnica" },
                new SkillScore { Name = "Resolucao de problemas", Score = System.Math.Min(98, baseScore + 10), Group = "Pratica" },
                new SkillScore { Name = "Inteligencia emocional", Score = System.Math.Min(94, baseScore - 4), Group = "Humana" },
                new SkillScore { Name = "Trabalho em equipe", Score = System.Math.Min(95, baseScore + 1), Group = "Humana" },
                new SkillScore { Name = "Performance sob pressao", Score = System.Math.Min(96, baseScore - 1), Group = "Pratica" }
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
                new TechElo { Technology = "Comunicacao", Rating = programmer.EloRating - 130, Rank = EloService.GetRank(programmer.EloRating - 130) }
            };
        }

        private static List<IntegrationLink> BuildIntegrations(ProgrammerProfile programmer)
        {
            return new List<IntegrationLink>
            {
                new IntegrationLink { Name = "GitHub", Url = programmer.FakeGitHub, Status = "Simulado", Description = "Futuro: importar repos e sinais de contribuicao." },
                new IntegrationLink { Name = "LinkedIn", Url = programmer.FakeLinkedIn, Status = "Simulado", Description = "Futuro: comparar narrativa profissional com performance real." },
                new IntegrationLink { Name = "Portfolio", Url = programmer.FakePortfolio, Status = "Simulado", Description = "Futuro: anexar projetos, cases e evidencias." },
                new IntegrationLink { Name = "Curriculo", Url = "curriculo-" + programmer.Username + ".pdf", Status = "Pendente", Description = "Futuro: gerar relatorio tecnico exportavel." }
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
    }
}
