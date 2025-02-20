using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AquaFeedShop.core.Models;
using AquaFeedShop.shared.Models.Request;

namespace AquaFeedShop.services.Interfaces
{
    public interface ICartService
    {
        Task<Cart?> CreateCart(Cart cart);
        Task<IEnumerable<object>> GetAllCart();
        Task<IEnumerable<Cart>> GetCartByUserId(int userId);
        Task<bool> UpdateCart(Cart cart);

    }
}
