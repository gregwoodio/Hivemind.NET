using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Factories;
using System;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class GangsController : ApiController
    {
        private GangFactory _gangFactory;

        public GangsController(GangFactory gangFactory)
        {
            if (gangFactory == null)
            {
                throw new ArgumentNullException(nameof(gangFactory));
            }
            _gangFactory = gangFactory;
        }

        [HttpGet]
        public Gang GetGang(string id)
        {
            return _gangFactory.GetGang(id);
        }

        [HttpPost]
        public Gang AddGang([FromUri] string gangName, [FromUri] GangHouse house)
        {
            var gang = new Gang()
            {
                Name = gangName,
                House = house,
            };
            return _gangFactory.AddGang(gang);
        }

        [HttpPut]
        public Gang UpdateGang(Gang gang)
        {
            return _gangFactory.UpdateGang(gang);
        }
    }
}
