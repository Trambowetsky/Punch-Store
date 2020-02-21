using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Abstract
{
    public interface IOrderProcessor
    {
        IEnumerable<Order> Orders { get; }
        void SaveOrder(Cart cart, ShippingDetails shippingDetails, Order order);
        Order DeleteOrder(int OrderId);
    }
}
