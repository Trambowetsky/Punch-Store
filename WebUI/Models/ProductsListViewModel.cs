using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace WebUI.Models
{
    public class ProductsListViewModel
    {
        public IEnumerable<SportsProduct> Products { get; set; }
        public PagInfo PagInfo { get; set; }
        public string CurrentCategory { get; set; }
        public bool? IsFav { get; set; }
    }
}