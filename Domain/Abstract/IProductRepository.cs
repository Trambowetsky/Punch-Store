﻿using Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace Domain.Abstract
{
    public interface IProductRepository
    {
        IEnumerable<SportsProduct> SportsProducts { get; }
        void SaveProducts(SportsProduct sproduct);
        SportsProduct DeleteProduct(int prodId);
    }
}
