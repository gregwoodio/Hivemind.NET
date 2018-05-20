using Hivemind.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Security.Claims;

namespace WebApi.Controllers
{
    [Route("api/user")]
    public class UsersController : Controller
    {
        private IUserManager _userManager;

        public UsersController(IUserManager userManager)
        {
            if (userManager == null)
            {
                throw new ArgumentNullException(nameof(userManager));
            }
            _userManager = userManager;
        }

        [HttpPost]
        [Route("")]
        public Hivemind.Contracts.User Register(Hivemind.Entities.Login user)
        {
            return _userManager.RegisterUser(user);
        }

        [Authorize]
        [HttpGet]
        [Route("")]
        public Hivemind.Contracts.User GetUser()
        {
            var user = Request.HttpContext.Authentication.HttpContext.User as ClaimsPrincipal;
            var id = user.Claims.FirstOrDefault(claim => claim.Type == "userId").Value;

            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            return _userManager.GetUser(id);
        }
    }
}
