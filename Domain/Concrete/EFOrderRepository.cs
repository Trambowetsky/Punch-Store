using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFOrderRepository
    {
        EFDBContext context = new EFDBContext();
        public IEnumerable<Order> Orders
        {
            get { return context.Orders; }
        }
    }
}
