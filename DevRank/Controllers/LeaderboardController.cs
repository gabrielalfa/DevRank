using System.Web.Mvc;
using Db = DevRank.Data.AppData;

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
