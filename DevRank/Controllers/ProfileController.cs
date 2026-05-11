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

        public ActionResult Edit()
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

            return View(ToEditViewModel(programmer));
        }

        [HttpPost]
        public ActionResult Edit(ProfileEditViewModel model)
        {
            var userId = Session["UserId"] == null ? (int?)null : (int)Session["UserId"];

            if (!userId.HasValue)
            {
                return RedirectToAction("Login", "Account");
            }

            model.Id = userId.Value;

            if (string.IsNullOrWhiteSpace(model.Name) ||
                string.IsNullOrWhiteSpace(model.Username) ||
                string.IsNullOrWhiteSpace(model.MainStack))
            {
                model.ErrorMessage = "Informe nome, username e stack principal.";
                return View(model);
            }

            if (Db.UsernameExistsForAnotherProgrammer(model.Username, model.Id))
            {
                model.ErrorMessage = "Esse username já está em uso por outro perfil.";
                return View(model);
            }

            model.Bio = string.IsNullOrWhiteSpace(model.Bio) ? "Desenvolvedor em evolução na arena DevRank." : model.Bio;
            model.SecondaryStack = string.IsNullOrWhiteSpace(model.SecondaryStack) ? "Em descoberta" : model.SecondaryStack;
            model.ExperienceTime = string.IsNullOrWhiteSpace(model.ExperienceTime) ? "Não informado" : model.ExperienceTime;

            Db.UpdateProgrammer(model);
            Session["UserName"] = model.Name;

            return RedirectToAction("Index");
        }

        private static ProfileEditViewModel ToEditViewModel(ProgrammerProfile programmer)
        {
            return new ProfileEditViewModel
            {
                Id = programmer.Id,
                Name = programmer.Name,
                Username = programmer.Username,
                Bio = programmer.Bio,
                MainStack = programmer.MainStack,
                SecondaryStack = programmer.SecondaryStack,
                ExperienceTime = programmer.ExperienceTime,
                FakeLinkedIn = programmer.FakeLinkedIn,
                FakeGitHub = programmer.FakeGitHub,
                FakePortfolio = programmer.FakePortfolio,
                LanguagesText = string.Join(", ", programmer.Languages ?? new string[0]),
                SoftSkillsText = "Comunicação, liderança, trabalho em equipe, inteligência emocional",
                AvatarPreview = programmer.FakePhotoUrl
            };
        }
    }
}
