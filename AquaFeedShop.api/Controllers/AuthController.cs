using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using AquaFeedShop.shared.Models.Request;
using AquaFeedShop.shared.Models.Response;
using AquaFeedShop.core.Models;
using AquaFeedShop.services.Interfaces;
using Google.Apis.Auth;
using AquaFeedShop.shared.Extensions;
using AquaFeedShop.services;
using Microsoft.Extensions.Configuration;

namespace AquaFeedShop.api.Controllers
{
    [Route("api/auth")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly IConfiguration _configuration;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        public AuthController(IConfiguration configuration,
           IAuthService authService,
           IUserService userService)
        {
            _configuration = configuration;
            _authService = authService;
            _userService = userService;
        }


        [HttpPost("login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            ApiResponse<string> response = new ApiResponse<string>();
            try
            {
                if (!IsPasswordValid(model.Password))
                {
                    throw new Exception("Password must be at least 8 characters long, and include at least one uppercase letter, one lowercase letter, and one number.");
                }

                var user = await _authService.GetUserByEmailAsync(model.Email);
                if (user == null) throw new Exception("Email is incorrect or not registered.");
                //if (user.Password != null && !PasswordHasher.VerifyPassword(model.Password, user.Password)) throw new Exception("Email or password is not correct.");
                //TokenModel token = new TokenModel
                //{
                //    AccessToken = await GenerateAccessToken(user),
                //    RefreshToken = await GenerateRefreshToken()
                //};
                //await _authService.SaveRefreshToken(user.UserId, token.RefreshToken);
                //response.Data = token.AccessToken;

                //Response.Cookies.Append("AccessToken", token.AccessToken, new CookieOptions
                //{
                //    HttpOnly = true,
                //    Secure = true,
                //    SameSite = SameSiteMode.None,
                //    Path = "/",
                //    Expires = DateTime.Now.AddDays(7)
                //});

                //Response.Cookies.Append("RefreshToken", token.RefreshToken, new CookieOptions
                //{
                //    HttpOnly = true,
                //    Secure = true,
                //    SameSite = SameSiteMode.None,
                //    Path = "/",
                //    Expires = DateTime.Now.AddDays(7)
                //});

                return Ok(response);
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.ErrorMessage = ex.Message;
                return Unauthorized(response);
            }
        }

        private bool IsPasswordValid(string password)
        {
            if (string.IsNullOrWhiteSpace(password)) return false;

            if (password.Length < 8) return false;

            if (!password.Any(char.IsUpper)) return false;

            if (!password.Any(char.IsLower)) return false;

            if (!password.Any(char.IsDigit)) return false;

            return true;
        }
    }
}
