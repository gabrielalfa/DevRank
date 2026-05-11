using System.Web.Mvc;
using Db = DevRank.FakeDatabase.FakeDatabase;

namespace DevRank.Controllers
{
    public class LeaderboardController : Controller
    {
        public ActionResult Index()
        {
            return View(Db.GetTopProgrammers(50));
        }
    }
}
