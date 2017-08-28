using Hivemind.Entities;
using Hivemind.Factories;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class GangsController : ApiController
    {
        private GangFactory _gangFactory;

        public GangsController(GangFactory gangFactory)
        {
            _gangFactory = gangFactory;
        }

        [HttpGet]
        public Gang GetGang(int id)
        {
            return _gangFactory.GetGang(id);
        }
    }
}
