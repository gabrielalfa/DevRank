using System.Linq;
using System.Web.Mvc;
using DevRank.Models;
using Db = DevRank.FakeDatabase.FakeDatabase;

namespace DevRank.Controllers
{
    public class DashboardController : Controller
    {
        public ActionResult Index()
        {
            var userId = Session["UserId"] == null ? (int?)null : (int)Session["UserId"];

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
                NextInterviewPrompt = "Explique uma decisao arquitetural dificil sem soar defensivo.",
                CoachInsight = "Seu perfil mostra boa entrega tecnica. O proximo salto e explicar trade-offs com mais clareza sob pressao."
            };

            return View(model);
        }
    }
}
