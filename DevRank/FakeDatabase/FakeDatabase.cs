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
                MainStack = "ASP.NET MVC",
                Bio = "Especialista em refatoracao de sistemas legados.",
                EloRating = 2310,
                Wins = 48,
                Losses = 12,
                Badges = new[] { "Legacy Slayer", "MVC Elite", "Clean Fix" }
            },
            new ProgrammerProfile
            {
                Id = 2,
                Username = "bruno",
                Password = "123",
                Name = "Bruno Stack",
                MainStack = "C# / SQL",
                Bio = "Resolve gargalos antes do cafe esfriar.",
                EloRating = 1975,
                Wins = 34,
                Losses = 15,
                Badges = new[] { "Perf Hunter", "Query Tamer" }
            },
            new ProgrammerProfile
            {
                Id = 3,
                Username = "carla",
                Password = "123",
                Name = "Carla Thread",
                MainStack = "Backend",
                Bio = "Boa em concorrencia, filas e bugs intermitentes.",
                EloRating = 1740,
                Wins = 28,
                Losses = 14,
                Badges = new[] { "Race Detector", "Service Mind" }
            },
            new ProgrammerProfile
            {
                Id = 4,
                Username = "diego",
                Password = "123",
                Name = "Diego Razor",
                MainStack = "Razor / JavaScript",
                Bio = "Transforma tela quebrada em fluxo usavel.",
                EloRating = 1510,
                Wins = 19,
                Losses = 11,
                Badges = new[] { "UI Fixer", "Vanilla JS" }
            },
            new ProgrammerProfile
            {
                Id = 5,
                Username = "eva",
                Password = "123",
                Name = "Eva Commit",
                MainStack = "Full Stack",
                Bio = "Entrega solucoes simples, testaveis e diretas.",
                EloRating = 1320,
                Wins = 14,
                Losses = 13,
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
                MainStack = model.MainStack,
                Bio = model.Bio,
                EloRating = 1000,
                Wins = 0,
                Losses = 0,
                Level = "Bronze",
                Badges = new[] { "Rookie", "Arena Ready" }
            };

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
            }
        }
    }
}
