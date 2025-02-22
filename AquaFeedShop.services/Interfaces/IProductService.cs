﻿using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquaFeedShop.core.Models;
using AquaFeedShop.shared.Models.Request;

namespace AquaFeedShop.services.Interfaces
{
    public interface IProductService
    {
        Task<IEnumerable<object>> GetAllProduct();
        Task<object> GetProductById(int id);
        Task<Product> GetProductByProductId(int id);

    }
}
