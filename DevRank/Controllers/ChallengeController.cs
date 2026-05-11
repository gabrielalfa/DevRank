using System.Linq;
using System.Web.Mvc;
using DevRank.Models;
using DevRank.Services;
using Db = DevRank.Data.AppData;

namespace DevRank.Controllers
{
    public class ChallengeController : Controller
    {
        private readonly EloService _eloService = new EloService();
        private readonly FakeAIService _fakeAIService = new FakeAIService();

        public ActionResult Index()
        {
            var userId = Session["UserId"] == null ? (int?)null : (int)Session["UserId"];
            var programmer = userId.HasValue ? Db.GetProgrammer(userId.Value) : null;
            var currentRating = programmer == null ? 0 : programmer.EloRating;
            ViewBag.CurrentRating = currentRating;

            return View(Db.Challenges.OrderBy(challenge => challenge.MinimumRating).ToList());
        }

        public ActionResult Details(int id)
        {
            var challenge = Db.GetChallenge(id);

            if (challenge == null)
            {
                return RedirectToAction("Index");
            }

            return View(new ChallengeDetailsViewModel { Challenge = challenge });
        }

        [HttpPost]
        public ActionResult Submit(int id, string solution)
        {
            var userId = Session["UserId"] == null ? (int?)null : (int)Session["UserId"];

            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var programmer = Db.GetProgrammer(userId.Value);
            var challenge = Db.GetChallenge(id);

            if (programmer == null)
            {
                Session.Clear();
                return RedirectToAction("Login", "Account");
            }

            if (challenge == null)
            {
                return RedirectToAction("Index");
            }

            if (string.IsNullOrWhiteSpace(solution))
            {
                return View("Details", new ChallengeDetailsViewModel
                {
                    Challenge = challenge,
                    ErrorMessage = "Escreva uma proposta de solução antes de enviar."
                });
            }

            var eloBefore = programmer.EloRating;
            var match = _fakeAIService.Evaluate(programmer, challenge, solution);
            var eloAfter = match.Approved
                ? _eloService.CalculateVictory(programmer.EloRating, challenge.ChallengeRating)
                : _eloService.CalculateDefeat(programmer.EloRating, challenge.ChallengeRating);

            programmer.EloRating = eloAfter;
            programmer.Level = _eloService.GetRank(eloAfter);

            if (match.Approved)
            {
                programmer.Wins++;
            }
            else
            {
                programmer.Losses++;
            }

            match.EloBefore = eloBefore;
            match.EloAfter = eloAfter;
            match.EloDelta = _eloService.CalculateDelta(eloBefore, eloAfter);
            Db.SaveMatch(match);

            var model = new SubmissionResultViewModel
            {
                Challenge = challenge,
                Match = match,
                Programmer = programmer
            };

            return View("Result", model);
        }
    }
}
