using Hivemind.Entities;
using Hivemind.Exceptions;
using Hivemind.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Api.Core.Controllers
{
    [AllowAnonymous]
    [Route("api/login")]
    public class TokenController : Controller
    {
        private IUserManager _userManager;
        private IConfiguration _configuration;

        public TokenController(IUserManager userManager, IConfiguration configuration)
        {
            _userManager = userManager;
            _configuration = configuration;
        }

        [HttpPost]
        public JsonResult RequestToken([FromForm] Login request)
        {
            ValidateRequest(request);

            var user = _userManager.Login(new Login()
            {
                Email = request.Email,
                Password = request.Password,
            });

            var claims = new[]
            {
                new Claim(ClaimTypes.Name, request.Email),
                new Claim("userId", user.UserGUID)
            };

            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SecurityKey"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var token = new JwtSecurityToken(
                issuer: "yourdomain.com",
                audience: "yourdomain.com",
                claims: claims,
                expires: DateTime.Now.AddMinutes(30),
                signingCredentials: creds);

            return Json(new 
            {
                access_token = new JwtSecurityTokenHandler().WriteToken(token)
            });
        }

        private void ValidateRequest(Login request)
        {
            if (string.IsNullOrEmpty(request.Email) || string.IsNullOrEmpty(request.Password))
            {
                HivemindException.InvalidUsernameOrPassword();
            }
        }
    }
}
