using Hivemind.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Security.Claims;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("api/user")]
    public class UsersController : ApiController
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
            var user = Request.GetRequestContext().Principal as ClaimsPrincipal;
            var id = user.Claims.FirstOrDefault(claim => claim.Type == "userId").Value;

            if (string.IsNullOrEmpty(id))
            {
                return null;
            }

            return _userManager.GetUser(id);
        }
    }
}
