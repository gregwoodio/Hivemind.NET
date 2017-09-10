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
    [RoutePrefix("api/territories")]
    public class TerritoriesController : ApiController
    {
        private ITerritoryFactory _territoryFactory;

        public TerritoriesController(ITerritoryFactory territoryFactory)
        {
            if (territoryFactory == null)
            {
                throw new ArgumentNullException(nameof(territoryFactory));
            }
            _territoryFactory = territoryFactory;
        }

        [HttpGet]
        public IEnumerable<Territory> GetAllTerritories()
        {
            return _territoryFactory.GetAllTerritories();
        }

        [HttpGet]
        [Route("{gangId}")]
        public IEnumerable<GangTerritory> GetGangTerritoryById([FromUri] string gangId)
        {
            return _territoryFactory.GetTerritoriesByGangId(gangId);
        }

        [HttpPost]
        [Route("{gangId}")]
        public GangTerritory AddGangTerritory([FromUri]string gangId, Territory territory)
        {
            var gangTerritory = new GangTerritory()
            {
                Territory = territory,
                GangId = gangId
            };
            return _territoryFactory.AddGangTerritory(gangTerritory);
        }

        [HttpDelete]
        [Route("{gangTerritoryId}")]
        public void RemoveGangTerritory([FromUri] string gangTerritoryId)
        {
            _territoryFactory.RemoveGangTerritory(gangTerritoryId);
        }
    }
}
