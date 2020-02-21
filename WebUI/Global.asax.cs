using Domain.Concrete;
using Domain.Entities;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using System.Web.Routing;
using WebUI.App_Start;
using WebUI.Infrastructure;

namespace WebUI
{
    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            BotStart.bot = new Models.StoreBot();
            BotStart.bot.RegisterAcc += Bot_RegisterAcc;
            BotStart.bot.Start();
            AreaRegistration.RegisterAllAreas();
            Database.SetInitializer<EFDBContext>(new CreateDatabaseIfNotExists<EFDBContext>());
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            ModelBinders.Binders.Add(typeof(Cart), new CartModelBinder());
            using (EFDBContext db = new EFDBContext())
            {
                if (db.Users.Count() == 0)
                {
                    db.Users.Add(new User { UserName = "admin", Password = CypherClass.XORCipher("Supersecretp@ssw0rd!", "tramb") });
                    db.SaveChanges();
                }
            }
        }

        private void Bot_RegisterAcc(string key, long userId, long chatId)
        {
            using (EFDBContext db = new EFDBContext())
            {
                User user = db.Users.ToList().FirstOrDefault(a => a.Key == key);
                if (user == null)
                {
                    BotStart.bot.SendMessage("Ключ хибний", chatId);
                }
                else
                {
                    user.UserId = userId;
                    user.ChatId = chatId;
                    db.SaveChanges();
                    BotStart.bot.SendMessage("Ви вдало зареєструвалися", chatId);
                }
            }
        }
    }
}
