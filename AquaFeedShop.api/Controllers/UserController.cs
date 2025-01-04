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


namespace AquaFeedShop.api.Controllers
{
    [Route("api/user")]
    [ApiController]
    public class UserController : ControllerBase
    {

        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        private readonly Cloudinary _cloudinary;

        public UserController(IUserService userService, IMapper mapper)
        {

            _userService = userService;

            _mapper = mapper;


            var cloudinaryAccount = new Account(
               "dan0stbfi",  
               "687237422157452",
               "XQbEo1IhkXxbC24rHvpdNJ5BHNw"   
           );
            _cloudinary = new Cloudinary(cloudinaryAccount);

        }

        
        [HttpGet]
        public async Task<IActionResult> GetUserList()
        {
            var usersList = await _userService.GetAllUsers();
            if (usersList == null)
            {
                return NotFound();
            }
            ApiResponse<IEnumerable<User>> response = new ApiResponse<IEnumerable<User>>();
            response.Data = usersList;
            return Ok(response);
        }   
    }
}
