using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web;
using System.Web.Mvc;
using WebUI.App_Start;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class CartController : Controller
    {
        private IProductRepository repository;
        EFDBContext context = new EFDBContext();
        private IOrderProcessor orderProcessor;

        public CartController(IProductRepository repo, IOrderProcessor orderprocess)
        {
            repository = repo;
            orderProcessor = orderprocess;
        }

        public ViewResult Index(Cart cart, string returnUrl)
        {
            return View(new CartIndexViewModel
            {
                Cart = cart,
                ReturnUrl = returnUrl
            });
        }

        public RedirectToRouteResult AddToCart(Cart cart, int productId, string returnUrl)
        {
            SportsProduct product = repository.SportsProducts
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.AddItem(product, 1);
            }

            return RedirectToAction("Summary", new { cart });
        }

        public RedirectToRouteResult RemoveFromCart(Cart cart, int productId, string returnUrl)
        {
            SportsProduct product = repository.SportsProducts
                .FirstOrDefault(p => p.ProductID == productId);

            if (product != null)
            {
                cart.RemoveLine(product);
            }

            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToRouteResult MinusQuantity(int quant, Cart cart, int productId, string returnUrl)
        {
            bool isOne = false;
            quant = 1;
            SportsProduct product = repository.SportsProducts
                .FirstOrDefault(p => p.ProductID == productId);
                cart.MinusItem(ref isOne, product, quant);
            if (isOne)
                cart.RemoveLine(product);
            return RedirectToAction("Index", new { returnUrl });
        }
        public RedirectToRouteResult PlusQuantity(int quant, Cart cart, int productId, string returnUrl)
        {
            quant = 1;
            SportsProduct product = repository.SportsProducts
                .FirstOrDefault(p => p.ProductID == productId);
            if (product != null)
            {
                cart.AddItem(product, quant);
            }
            return RedirectToAction("Index", new { returnUrl });
        }
        public ActionResult Summary(Cart cart)
        {
            return PartialView(cart);
        }

        public ViewResult Checkout()
        {
            return View(new ShippingDetails());
        }

        [HttpPost]
        public ViewResult Checkout(Cart cart, ShippingDetails shippingDetails)
        {
            StringBuilder builder = new StringBuilder();
            Order order = new Order();
            EFProductRepository eFProduct = new EFProductRepository();
            if (cart.Lines.Count() == 0)
            {
                ModelState.AddModelError("", "Вибачте, кошик пустий");
            }

            if (ModelState.IsValid)
            {
                orderProcessor.SaveOrder(cart, shippingDetails, order);
               
                using (UserContext db = new UserContext())
                {
                    builder.AppendLine("На сайті зроблено замовлення!");
                    foreach (var lines in cart.Lines)
                    {
                        builder.AppendLine($"-  {lines.Product.ProductName} - {lines.Quantity} шт.");
                    }
                    builder.AppendLine($"http://{HttpContext.Request.Url.Authority}/Admin/Purchases");
                    string message = builder.ToString();
                    foreach (var user in db.Users)
                    {
                        if (user.ChatId != 0)
                        {
                            BotStart.bot.SendMessage(message, user.ChatId);
                        }
                    }
                }
                cart.Clear();   
                return View("Completed");
            }
            else
            {
                return View(new ShippingDetails());
            } 
        }
    }
}