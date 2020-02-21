using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class EFDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<SportsProduct> SportsProducts { get; set; }
        public DbSet<Order> Orders { get; set; }
    }
}
