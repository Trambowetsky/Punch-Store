using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using Domain.Concrete;
using WebUI.Models;

namespace WebUI.Controllers
{
    public class ProductsController : Controller
    {
        private IProductRepository repository;
        public int pageSize = 12;

        public ProductsController(IProductRepository repo)
        {
            repository = repo;
        }

        public ViewResult List(string category, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.SportsProducts
                .Where(b => category == null || b.PartOfBody == category)
                .OrderByDescending(product => product.ProductID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagInfo = new PagInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = category == null ?
                        repository.SportsProducts.Count() :
                        repository.SportsProducts.Where(product => product.PartOfBody == category).Count()
                },
                CurrentCategory = category
            };

            return View(model);
        }
        public ViewResult Favourites(bool? fav, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.SportsProducts
                .Where(product => product.IsFavourite == true)
                .OrderBy(product => product.ProductID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagInfo = new PagInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = fav == true ?
                        repository.SportsProducts.Count() :
                        repository.SportsProducts.Where(product => product.IsFavourite == fav).Count()
                },
                IsFav = fav
            };

            return View("List", model);
        }
        public FileContentResult GetImage(int productId, int pictureNumber)
        {
            SportsProduct sp = repository.SportsProducts
                .FirstOrDefault(g => g.ProductID == productId);

            if (sp != null)
            {
                switch (pictureNumber)
                {
                    case 1:
                        if (sp.ImageData != null && sp.ImageMimeType != null)
                            return File(sp.ImageData, sp.ImageMimeType);
                        else return null;
                    case 2:
                        if (sp.ImageData_2 != null && sp.ImageMimeType_2 != null)
                            return File(sp.ImageData_2, sp.ImageMimeType_2);
                        else return null;
                    case 3:
                        if (sp.ImageData_3 != null && sp.ImageMimeType_3 != null)
                            return File(sp.ImageData_3, sp.ImageMimeType_3);
                        else return null;
                    case 4:
                        if (sp.ImageData_4 != null && sp.ImageMimeType_4 != null)
                            return File(sp.ImageData_4, sp.ImageMimeType_4);
                        else return null;
                }
                return null;
            }
            else
            {
                return null;
            }
        }
        public ActionResult ProductView(int productId)
        {
            SportsProduct sp = repository.SportsProducts
                .FirstOrDefault(g => g.ProductID == productId);
            return View(sp);
        }
        public ViewResult Search(string searchString, int page = 1)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                Products = repository.SportsProducts
                .Where(product => product.ProductName.ToLower().Contains(searchString.ToLower()))
                .OrderBy(product => product.ProductID)
                .Skip((page - 1) * pageSize)
                .Take(pageSize),
                PagInfo = new PagInfo
                {
                    CurrentPage = page,
                    ItemsPerPage = pageSize,
                    TotalItems = repository.SportsProducts.Count()
                }
            };
            return View("List", model);
        }
        public PartialViewResult AdditionalItems(string category, int currentID)
        {
            ProductsListViewModel model = new ProductsListViewModel
            {
                 Products = repository.SportsProducts
                .Where(g => g.PartOfBody == category && g.ProductID != currentID)
                .Randomize()
                .Take(4)
            };
            return PartialView(model);
        }
        public ViewResult Categories()
        {
            return View();
        }
    }
}