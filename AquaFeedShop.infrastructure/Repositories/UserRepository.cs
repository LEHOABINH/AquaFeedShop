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
    public class UserRepository : GenericRepository<User>, IUserRepository
    {
        public UserRepository(AquaFeedShopContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<User>> GetAllUsers()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<User?> GetUserByEmailAsync(string email)
        {
            return await dbSet.FirstOrDefaultAsync(u =>
                u.Email.ToLower() == email.ToLower());
        }
    }
}
