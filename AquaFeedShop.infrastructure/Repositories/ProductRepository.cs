using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquaFeedShop.core.Interfaces;
using AquaFeedShop.core.Models;

namespace AquaFeedShop.infrastructure.Repositories
{
    public class ProductRepository : GenericRepository<Product>, IProductRepository
    {
        public ProductRepository(AquaFeedShopContext dbContext) : base(dbContext)
        {
        }
    }
}
