using System.Linq;
using System.Web.Mvc;
using DevRank.Models;
using Db = DevRank.FakeDatabase.FakeDatabase;

namespace DevRank.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            var topProgrammers = Db.GetTopProgrammers(5);

            var model = new HomeViewModel
            {
                TopProgrammers = topProgrammers,
                TotalDevelopers = Db.Programmers.Count,
                TotalChallenges = 6,
                TotalMatches = topProgrammers.Sum(programmer => programmer.TotalMatches)
            };

            return View(model);
        }
    }
}
