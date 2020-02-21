using Domain.Abstract;
using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class OrderProcessor : IOrderProcessor
    {
        EFDBContext efdb = new EFDBContext();
            
        public IEnumerable<Order> Orders => efdb.Orders;

        public Order DeleteOrder(int OrderId)
        {
            Order dbEntry = efdb.Orders.Find(OrderId);
            if (dbEntry != null)
            {
                efdb.Orders.Remove(dbEntry);
                efdb.SaveChanges();
            }
            return dbEntry;
        }

        public void SaveOrder(Cart cart, ShippingDetails shippingDetails, Order order)
        {
            foreach (var lines in cart.Lines)
            {
                order.City = shippingDetails.City;
                order.Address = shippingDetails.Line1;
                order.Country = shippingDetails.Country;
                order.Name = shippingDetails.Name;
                order.Phone = shippingDetails.PhoneNumber;
                order.Quantity = lines.Quantity;
                order.ProductID = lines.Product.ProductID;
                order.ProdName = lines.Product.ProductName;
                order.OrdTime = DateTime.Now;
                order.TotalPrice = (int)cart.ComputeTotalValue();
                efdb.Orders.Add(order);
                efdb.SaveChanges();
            }

        }
    }
}
