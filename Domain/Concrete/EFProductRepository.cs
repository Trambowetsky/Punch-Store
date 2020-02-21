using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFProductRepository : IProductRepository
    {
        EFDBContext context = new EFDBContext();
        public IEnumerable<SportsProduct> SportsProducts
        {
            get { return context.SportsProducts;  }
        }


        public SportsProduct DeleteProduct(int prodId)
        {
            SportsProduct dbEntry = context.SportsProducts.Find(prodId);
            if (dbEntry != null)
            {
                context.SportsProducts.Remove(dbEntry);
                context.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveProducts(SportsProduct sproduct)
        {
            if (sproduct.ProductID == 0)
            {
                context.SportsProducts.Add(sproduct);
            }
            else
            {
                SportsProduct dbEntry = context.SportsProducts.Find(sproduct.ProductID);
                if (dbEntry != null)
                {
                    dbEntry.ProductName = sproduct.ProductName;
                    dbEntry.ManufCountry = sproduct.ManufCountry;
                    dbEntry.Manufacturer = sproduct.Manufacturer;
                    dbEntry.Price = sproduct.Price;
                    dbEntry.Description = sproduct.Description;
                    dbEntry.PartOfBody = sproduct.PartOfBody;
                    dbEntry.LongDesc = sproduct.LongDesc;
                    dbEntry.IsFavourite = sproduct.IsFavourite;
                    if (sproduct.ImageData != null && sproduct.ImageMimeType != null)
                    {
                        dbEntry.ImageData = sproduct.ImageData;
                        dbEntry.ImageMimeType = sproduct.ImageMimeType;
                    }
                    if (sproduct.ImageData_2 != null && sproduct.ImageMimeType_2 != null)
                    {
                        dbEntry.ImageData_2 = sproduct.ImageData_2;
                        dbEntry.ImageMimeType_2 = sproduct.ImageMimeType_2;
                    }
                    if (sproduct.ImageData_3 != null && sproduct.ImageMimeType_3 != null)
                    {
                        dbEntry.ImageData_3 = sproduct.ImageData_3;
                        dbEntry.ImageMimeType_3 = sproduct.ImageMimeType_3;
                    }
                    if (sproduct.ImageData_4 != null && sproduct.ImageMimeType_4 != null)
                    {
                        dbEntry.ImageData_4 = sproduct.ImageData_4;
                        dbEntry.ImageMimeType_4 = sproduct.ImageMimeType_4;
                    }
                }
            }
            context.SaveChanges();
        }
    }
}
