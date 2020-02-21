using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class AccountController : Controller
    {
        UserContext context;
        public AccountController(UserContext cont)
        {
            context = cont;
        }
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Login(LoginModel model)
        {
            if (ModelState.IsValid)
            {
                // поиск пользователя в бд
                User user = null;
                using (UserContext db = new UserContext())
                {
                    string cyphPass = CypherClass.XORCipher(model.Password, "tramb");
                    user = db.Users.FirstOrDefault(u => u.UserName == model.UserName && u.Password == cyphPass);

                }
                if (user != null)
                {
                    FormsAuthentication.SetAuthCookie(model.UserName, true);
                    return RedirectToAction("Index", "Admin");
                }
                else
                {
                    ModelState.AddModelError("", "Користувача з таким логіном та паролем не існує");
                }
            }

            return View(model);
        }
        [Authorize]
        public ActionResult Register()
        {
             return View("AccRegister");
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register(RegisterModel model)
        {
            if (ModelState.IsValid)
            {
                User user = null;
                using (UserContext db = new UserContext())
                {
                    user = db.Users.FirstOrDefault(u => u.UserName == model.UserName);
                }
                if (user == null)
                {
                    // создаем нового пользователя
                    using (EFDBContext db = new EFDBContext())
                    {
                        string cyphPass = CypherClass.XORCipher(model.Password, "tramb");
                        db.Users.Add(new User { UserName = model.UserName, Password = cyphPass});
                        db.SaveChanges();

                        user = db.Users.Where(u => u.UserName == model.UserName && u.Password == cyphPass).FirstOrDefault();
                    }
                    // если пользователь удачно добавлен в бд
                    if (user != null)
                    {
                        FormsAuthentication.SetAuthCookie(model.UserName, true);
                        return RedirectToAction("List", "Account");
                    }
                }
                else
                {
                    ModelState.AddModelError("", "Користувач з таким логіном вже існує");
                }
            }

            return View("AccRegister", model);
        }
        public ActionResult Logoff()
        {
            FormsAuthentication.SignOut();
            return RedirectToAction("List", "Products");
        }
        [Authorize]
        public ActionResult List()
        {
            return View(context.Users);
        }
        public ActionResult DeleteAcc(int id)
        {
            using (EFDBContext context = new EFDBContext())
            {
                User deletedUser = context.Users.Find(id);
                if (deletedUser != null)
                {
                    context.Users.Remove(deletedUser);
                    TempData["message"] = string.Format("Адміністратор був видалений");
                    context.SaveChanges();
                }
            }
            return RedirectToAction("List");
        }
    }
}