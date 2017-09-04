using Hivemind.Entities;
using Hivemind.Factories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace WebApi.Controllers
{
    public class GangersController : ApiController
    {
        private IGangerFactory _gangerFactory;

        public GangersController(IGangerFactory gangerFactory)
        {
            if (gangerFactory == null)
            {
                throw new ArgumentNullException(nameof(gangerFactory));
            }
            _gangerFactory = gangerFactory;
        }

        [HttpPost]
        public Ganger AddGanger(Ganger ganger)
        {
            return _gangerFactory.AddGanger(ganger);
        }

        [HttpGet]
        public Ganger GetGanger(string gangerId)
        {
            return _gangerFactory.GetGanger(gangerId);
        }

        [HttpPut]
        public Ganger UpdateGanger(Ganger ganger)
        {
            return _gangerFactory.UpdateGanger(ganger);
        }
    }
}
