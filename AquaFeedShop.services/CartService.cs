using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.SignalR;
using Microsoft.IdentityModel.Tokens;
using OfficeOpenXml;
using System.Text.RegularExpressions;
using System.Transactions;
using AquaFeedShop.core.Interfaces;
using AquaFeedShop.core.Models;

using AquaFeedShop.services.Interfaces;
using AquaFeedShop.shared.Extensions;
using AquaFeedShop.shared.Models.Request;
using AquaFeedShop.shared.Models.Response;
using static System.Runtime.InteropServices.JavaScript.JSType;
using Task = System.Threading.Tasks.Task;

namespace AquaFeedShop.services
{
    public class CartService : ICartService
    {
        public IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CartService(
            IUnitOfWork unitOfWork,
            IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<Cart?> CreateCart(Cart cart)
        {
            if (cart == null)
            {
                return null; // Trả về null nếu input không hợp lệ
            }

            await _unitOfWork.Carts.InsertAsync(cart);

            var result = await _unitOfWork.SaveAsync(); // Sử dụng SaveAsync để đảm bảo đồng bộ

            if (result > 0)
            {
                return cart; // Trả về cart nếu lưu thành công
            }

            return null; // Trả về null nếu lưu thất bại
        }

        public async Task<IEnumerable<object>> GetAllCart()
        {
            var cartList = await _unitOfWork.Carts.GetAsync(
                includeProperties: "Product,Product.Supplier"
            );

            return cartList;
        }

        public async Task<IEnumerable<Cart>> GetCartByUserId(int userId)
        {
            var carts = await _unitOfWork.Carts.GetAsync(s => s.UserId == userId);

            return carts;  
        }

        public async Task<bool> UpdateCart(Cart cart)
        {
            if (cart != null)
            {
                var cartUpdate = (await _unitOfWork.Carts.GetAsync(s => s.UserId == cart.UserId && s.ProductId == cart.ProductId)).FirstOrDefault();
                if (cartUpdate != null)
                {

                    cartUpdate.Quantity = cart.Quantity; 

                    _unitOfWork.Carts.Update(cartUpdate);

                    var result = await _unitOfWork.SaveAsync(); // Sử dụng SaveAsync nếu có hỗ trợ bất đồng bộ

                    if (result > 0)
                        return true;
                    else
                        return false;
                }
            }
            return false;
        }
    }
}
