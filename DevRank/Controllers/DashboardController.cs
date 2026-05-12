using System.Linq;
using System.Web.Mvc;
using DevRank.Models;
using DevRank.Services;
using Db = DevRank.Data.AppData;

namespace DevRank.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            var userId = AuthSessionService.GetCurrentUserId(this);

            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var programmer = Db.GetProgrammer(userId.Value);

            if (programmer == null)
            {
                Session.Clear();
                return RedirectToAction("Login", "Account");
            }

            var model = new DashboardViewModel
            {
                Programmer = programmer,
                RecentHistory = Db.GetHistoryForProgrammer(programmer.Id).Take(4).ToList(),
                RecommendedChallenges = Db.GetRecommendedChallenges(programmer.EloRating, 3),
                TopProgrammers = Db.GetTopProgrammers(5),
                CompletedModules = 4,
                TotalModules = 15,
                NextInterviewPrompt = "Explique uma decisão arquitetural difícil sem soar defensivo.",
                CoachInsight = "Seu perfil mostra boa entrega técnica. O próximo salto é explicar trade-offs com mais clareza sob pressão."
            };

            return View(model);
        }
    }
}
