using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using CloudinaryDotNet;
using CloudinaryDotNet.Actions;
using OfficeOpenXml;
using System.Security.Claims;
using AquaFeedShop.core.Models;
using AquaFeedShop.services.Interfaces;
using AquaFeedShop.shared.Extensions;
using Microsoft.AspNetCore.SignalR;
using AquaFeedShop.shared.Models.Response;
using AquaFeedShop.shared.Models.Request;
using Azure;


namespace AquaFeedShop.api.Controllers
{
    [Route("api/cart")]
    [ApiController]
    public class CartController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly ICartService _cartService;
        private readonly IMapper _mapper;

        public CartController(IUserService userService, ICartService cartService, IProductService productService, IMapper mapper)
        {

            _userService = userService;
            _productService = productService;
            _cartService = cartService;
            _mapper = mapper;

        }

        [HttpGet]
        public async Task<IActionResult> GetCartList()
        {
            var productList = await _cartService.GetAllCart();
            if (productList == null)
            {
                return NotFound();
            }
            ApiResponse<IEnumerable<object>> response = new ApiResponse<IEnumerable<object>>();
            response.Data = productList;
            return Ok(response);
        }

        [Authorize(Roles = "customer")]
        [HttpGet("MyCart")]
        public async Task<IActionResult> GetCartByUserId()
        {
            var userIdString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;
            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return BadRequest(new ApiResponse<object> { Success = false, ErrorMessage = "Unauthorized access." });
            }

            var cart = await _cartService.GetCartByUserId(userId);

            if (cart == null)
            {
                return NotFound();
            }
            ApiResponse<IEnumerable<Cart>> response = new ApiResponse<IEnumerable<Cart>>();
            response.Data = cart;
            return Ok(response);
        }

        [Authorize(Roles = "customer")]
        [HttpPost]
        public async Task<IActionResult> CreateCart([FromBody] CartModel newCart)
        {
            var userIdString = User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier)?.Value;

            if (string.IsNullOrEmpty(userIdString) || !int.TryParse(userIdString, out int userId))
            {
                return BadRequest(new ApiResponse<object> { Success = false, ErrorMessage = "Unauthorized access." });
            }

            var cartExist = await _cartService.GetCartByUserId(userId);

            if (cartExist != null)
            {
                var existingCartItem = cartExist.FirstOrDefault(c => c.ProductId == newCart.ProductId);
                if (existingCartItem != null)
                {
                    // Nếu newCart.Quantity bằng 0, tăng số lượng của existingCartItem lên 1
                    if (newCart.Quantity == 0)
                    {
                        existingCartItem.Quantity += 1;
                    }
                    else
                    {
                        existingCartItem.Quantity = existingCartItem.Quantity + newCart.Quantity;
                    }

                    var cartUpdate = await _cartService.UpdateCart(existingCartItem);
                    var response = new ApiResponse<object>
                    {
                        Data = new { Message = "Cart updated successfully!", Cart = cartUpdate }
                    };
                    return Ok(response);
                }
            }

            // Nếu không có giỏ hàng hoặc không trùng sản phẩm, tạo mới giỏ hàng
            var objCart = _mapper.Map<Cart>(newCart);
            var product = await _productService.GetProductByProductId(objCart.ProductId);
            objCart.UserId = userId;
            objCart.Price = product.Price;
            if (newCart.Quantity == 0)
            {
                objCart.Quantity = 1;
            }
            else
            {
                objCart.Quantity =  newCart.Quantity;
            }
            var cart = await _cartService.CreateCart(objCart);
            var responseCreate = new ApiResponse<object>
            {
                Data = new { Message = "Cart created successfully!", Cart = cart }
            };
            return Ok(responseCreate);
        }

    }
}
