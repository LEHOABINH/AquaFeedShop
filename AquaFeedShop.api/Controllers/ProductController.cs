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
using System.Data;


namespace AquaFeedShop.api.Controllers
{
    [Route("api/product")]
    [ApiController]
    public class ProductController : ControllerBase
    {

        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IMapper _mapper;

        public ProductController(IUserService userService, IProductService productService, IMapper mapper)
        {

            _userService = userService;
            _productService = productService;
            _mapper = mapper;

        }

        
        [HttpGet]
        public async Task<IActionResult> GetProductList()
        {
            var productList = await _productService.GetAllProduct();
            if (productList == null)
            {
                return NotFound();
            }
            ApiResponse<IEnumerable<object>> response = new ApiResponse<IEnumerable<object>>();
            response.Data = productList;
            return Ok(response);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductById(int id)
        {
            var user = await _userService.GetUserByRole(2);  // Lấy người dùng theo vai trò
            var product = await _productService.GetProductById(id);  // Lấy sản phẩm theo id

            if (product == null)
            {
                return NotFound();
            }

            // Tạo đối tượng response chứa cả user và product
            var responseData = new
            {
                User = user,
                Product = product
            };

            ApiResponse<object> response = new ApiResponse<object>
            {
                Data = responseData
            };

            return Ok(response);
        }


    }
}
