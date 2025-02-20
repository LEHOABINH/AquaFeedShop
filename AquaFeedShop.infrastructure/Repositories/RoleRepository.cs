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
    public class RoleRepository : GenericRepository<Role>, IRoleRepository
    {
        public RoleRepository(AquaFeedShopContext dbContext) : base(dbContext)
        {
        }
    }
}
