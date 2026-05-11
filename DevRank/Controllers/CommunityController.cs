using System;
using System.Web.Mvc;
using DevRank.Models;
using Db = DevRank.FakeDatabase.FakeDatabase;

namespace DevRank.Controllers
{
    public class CommunityController : Controller
    {
        public ActionResult Index()
        {
            var userId = GetCurrentUserId();

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

            return View(Db.GetCommunityHub(programmer));
        }

        public ActionResult CreateChallenge()
        {
            var userId = GetCurrentUserId();

            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            return View(new CommunityCreateChallengeViewModel
            {
                Type = "Cenário real",
                Category = "Backend",
                DailyLimitRemaining = 2,
                MinimumReputationRequired = 120
            });
        }

        [HttpPost]
        public ActionResult CreateChallenge(CommunityCreateChallengeViewModel model)
        {
            var userId = GetCurrentUserId();

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

            var validation = Db.ValidateCommunityChallenge(model, programmer);

            if (!string.IsNullOrWhiteSpace(validation))
            {
                model.ErrorMessage = validation;
                model.DailyLimitRemaining = 2;
                model.MinimumReputationRequired = 120;
                return View(model);
            }

            Db.SubmitCommunityChallenge(model, programmer);

            return View(new CommunityCreateChallengeViewModel
            {
                Type = model.Type,
                Category = model.Category,
                DailyLimitRemaining = 1,
                MinimumReputationRequired = 120,
                SuccessMessage = "Desafio enviado para shadow review. Revisores técnicos vão avaliar qualidade, clareza e relevância antes da publicação."
            });
        }

        public ActionResult Moderation()
        {
            var userId = GetCurrentUserId();

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

            return View(Db.GetCommunityHub(programmer));
        }

        [HttpPost]
        public ActionResult Moderate(int id, string decision)
        {
            var userId = GetCurrentUserId();

            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            var programmer = Db.GetProgrammer(userId.Value);
            Db.ModerateCommunityChallenge(id, decision, programmer);

            return RedirectToAction("Moderation");
        }

        private int? GetCurrentUserId()
        {
            return Session["UserId"] == null ? (int?)null : (int)Session["UserId"];
        }
    }
}
