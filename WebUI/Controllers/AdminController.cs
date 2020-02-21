using Domain.Abstract;
using Domain.Concrete;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebUI.Models;

namespace WebUI.Controllers
{
    [Authorize]
    public class AdminController : Controller
    {
        IProductRepository repository;
        IOrderProcessor processor;
        public AdminController(IOrderProcessor proc, IProductRepository repo)
        {
            processor = proc;
            repository = repo;
        }

        public ViewResult Index()
        {
            return View(repository.SportsProducts);
        }
        public ViewResult Edit(int productId)
        {
            SportsProduct sproduct = repository.SportsProducts.FirstOrDefault(p => p.ProductID == productId);
            return View(sproduct);
        }
        [HttpPost]
        public ActionResult Edit(SportsProduct sports, HttpPostedFileBase image = null, HttpPostedFileBase image_2 = null, HttpPostedFileBase image_3 = null, HttpPostedFileBase image_4 = null)
        {
            if (ModelState.IsValid)
            {
                if (image != null)
                {
                    sports.ImageMimeType = image.ContentType;
                    sports.ImageData = new byte[image.ContentLength];
                    image.InputStream.Read(sports.ImageData, 0, image.ContentLength);
                }
                if (image_2 != null)
                {
                    sports.ImageMimeType_2 = image_2.ContentType;
                    sports.ImageData_2 = new byte[image_2.ContentLength];
                    image_2.InputStream.Read(sports.ImageData_2, 0, image_2.ContentLength);
                }
                if (image_3 != null)
                {
                    sports.ImageMimeType_3 = image_3.ContentType;
                    sports.ImageData_3 = new byte[image_3.ContentLength];
                    image_3.InputStream.Read(sports.ImageData_3, 0, image_3.ContentLength);
                }
                if (image_4 != null)
                {
                    sports.ImageMimeType_4 = image_4.ContentType;
                    sports.ImageData_4 = new byte[image_4.ContentLength];
                    image_4.InputStream.Read(sports.ImageData_4, 0, image_4.ContentLength);
                }
                repository.SaveProducts(sports);
                TempData["message"] = string.Format("Редагування інформації продукту збережені.");
                return RedirectToAction("Index");
            }
            else
            {
                return View(sports);
            }
        }
        public ViewResult Create()
        {
            return View("Edit", new SportsProduct());
        }
        [HttpPost]
        public ActionResult Delete(int productId)
        {
            SportsProduct deletedProd = repository.DeleteProduct(productId);
            if (deletedProd != null)
            {
                TempData["message"] = string.Format("Товар {0} був видалений", deletedProd.ProductName);
            }
            return RedirectToAction("Index");
        }
        public ViewResult Purchases()
        {
            return View(processor.Orders);
        }
        [HttpPost]
        public ActionResult DeleteOrder(int OrderId)
        {
            Order deletedOrd = processor.DeleteOrder(OrderId);
            if (deletedOrd != null)
            {
                TempData["message"] = string.Format("Заказ {0} був видалений", deletedOrd.OrderId);
            }
            return RedirectToAction("Purchases");
        }
    }
}