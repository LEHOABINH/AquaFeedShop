using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquaFeedShop.core.Models;

namespace AquaFeedShop.services.Interfaces
{
    public interface IAuthService
    {
        //Task<User?> GetUserByRefreshToken(string? refreshToken);
        //System.Threading.Tasks.Task SaveRefreshToken(int id, string refreshToken);
        Task<User?> GetUserByEmailAsync(string email);
        //Task<bool> RevokeRefreshToken(int userId);

    }
}
