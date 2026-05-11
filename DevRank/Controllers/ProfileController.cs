using System.Web.Mvc;
using DevRank.Models;
using Db = DevRank.FakeDatabase.FakeDatabase;

namespace DevRank.Controllers
{
    public class ProfileController : Controller
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

            var model = new ProfileViewModel
            {
                Programmer = programmer,
                History = Db.GetHistoryForProgrammer(userId.Value)
            };

            return View(model);
        }

        public ActionResult Public(string username)
        {
            var programmer = Db.GetProgrammerByUsername(username);

            if (programmer == null)
            {
                return RedirectToAction("Index", "Leaderboard");
            }

            var model = new ProfileViewModel
            {
                Programmer = programmer,
                History = Db.GetHistoryForProgrammer(programmer.Id)
            };

            return View(model);
        }
    }
}
