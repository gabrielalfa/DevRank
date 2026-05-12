using System;
using System.Web.Mvc;
using System.Web.Security;
using DevRank.Models;
using Db = DevRank.Data.AppData;

namespace DevRank.Controllers
{
    public class AccountController : Controller
    {
        public ActionResult Login()
        {
            return View(new LoginViewModel());
        }

        [HttpPost]
        public ActionResult Login(LoginViewModel model)
        {
            var programmer = Db.Authenticate(model.Username, model.Password);

            if (programmer == null)
            {
                model.ErrorMessage = "Usuário ou senha inválidos. Para testar rápido: ana / 123.";
                return View(model);
            }

            Session["UserId"] = programmer.Id;
            Session["UserName"] = programmer.Name;
            FormsAuthentication.SetAuthCookie(programmer.Id.ToString(), model.RememberMe);
            return RedirectToAction("Index", "Profile");
        }

        public ActionResult Register()
        {
            return View(new RegisterViewModel());
        }

        [HttpPost]
        public ActionResult Register(RegisterViewModel model)
        {
            if (string.IsNullOrWhiteSpace(model.Username) ||
                string.IsNullOrWhiteSpace(model.Password) ||
                string.IsNullOrWhiteSpace(model.Name))
            {
                model.ErrorMessage = "Informe nome, usuário e senha para entrar na arena.";
                return View(model);
            }

            if (Db.UsernameExists(model.Username))
            {
                model.ErrorMessage = "Esse usuário já existe. Escolha outro handle.";
                return View(model);
            }

            model.MainStack = string.IsNullOrWhiteSpace(model.MainStack) ? "Full Stack" : model.MainStack;
            model.Bio = string.IsNullOrWhiteSpace(model.Bio) ? "Novo desafiante da arena DevRank." : model.Bio;

            var programmer = Db.Register(model);
            Session["UserId"] = programmer.Id;
            Session["UserName"] = programmer.Name;
            FormsAuthentication.SetAuthCookie(programmer.Id.ToString(), false);

            return RedirectToAction("Index", "Profile");
        }

        public ActionResult Logout()
        {
            FormsAuthentication.SignOut();
            Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}
