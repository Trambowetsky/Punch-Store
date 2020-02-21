using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Domain.Concrete
{
    public class UserContext : DbContext
    {
        EFDBContext context = new EFDBContext();
        public IEnumerable<User> Users
        {
            get { return context.Users; }
        }
    }
}
