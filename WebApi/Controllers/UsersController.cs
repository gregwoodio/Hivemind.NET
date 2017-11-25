using Hivemind.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
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
        public Hivemind.Contracts.User Register(Hivemind.Entities.User user)
        {
            return _userManager.RegisterUser(user);
        }
        
        [Authorize]
        [HttpGet]
        [Route("{userId}/gangs")]
        public IEnumerable<Hivemind.Contracts.User> GetUserGangs([FromUri] string userId)
        {
            throw new NotImplementedException();
        }
    }
}
