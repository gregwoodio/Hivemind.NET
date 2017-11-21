using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("user")]
    public class UsersController : ApiController
    {
        public UsersController()
        {

        }

        [HttpPost]
        public Hivemind.Contracts.User Register(Hivemind.Entities.User user)
        {
            throw new NotImplementedException();
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
