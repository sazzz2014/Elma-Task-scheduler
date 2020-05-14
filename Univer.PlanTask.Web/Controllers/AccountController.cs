using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using Univer.PlanTask.Web.Models;
using Univer.PlanTask.Web.NHibernate;

namespace Univer.PlanTask.Web.Controllers
{
   // [Authorize]
    public class AccountController : Controller
    {
        [AllowAnonymous]
        public ActionResult Login()
        {
            return View();
        }

        [AllowAnonymous]
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = null;

                var session = NHibernateHelper.GetCurrentSession();

                user = session
                    .Query<User>()
                    .FirstOrDefault(item => item.Login == model.Login);

                if (user != null)
                {
                    if (user.Password == model.Password)
                    {
                        FormsAuthentication.SetAuthCookie(model.Login, true);

                        Response.Cookies.Add(new HttpCookie("plannerUserFio", $" {user.FirstName} {user.SecondName}"));

                        return RedirectToAction("List", "Task");
                    }
                    else
                    {
                        ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Пользователя с таким логином и паролем нет");
                }
            }

            return View(model);
        }

        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("Login", "Account");
        }
    }
}