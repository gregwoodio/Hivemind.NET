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
    [RoutePrefix("api/gangers")]
    public class GangersController : ApiController
    {
        private IGangerFactory _gangerFactory;
        private IWeaponFactory _weaponFactory;

        public GangersController(IGangerFactory gangerFactory, IWeaponFactory weaponFactory)
        {
            if (gangerFactory == null)
            {
                throw new ArgumentNullException(nameof(gangerFactory));
            }
            if (weaponFactory == null)
            {
                throw new ArgumentNullException(nameof(weaponFactory));
            }
            _gangerFactory = gangerFactory;
            _weaponFactory = weaponFactory;
        }

        [HttpPost]
        public Ganger AddGanger(Ganger ganger)
        {
            return _gangerFactory.AddGanger(ganger);
        }

        [HttpGet]
        [Route("{gangerId}")]
        public Ganger GetGanger([FromUri] string gangerId)
        {
            return _gangerFactory.GetGanger(gangerId);
        }

        [HttpPut]
        public Ganger UpdateGanger(Ganger ganger)
        {
            return _gangerFactory.UpdateGanger(ganger);
        }

        // weapon routes
        [HttpGet]
        [Route("{gangerId}/weapons")]
        public IEnumerable<GangerWeapon> GetWeapons([FromUri] string gangerId)
        {
            return _weaponFactory.GetGangerWeapons(gangerId);
        }

        [HttpPost]
        [Route("{gangerId}/weapons")]
        public GangerWeapon AddGangerWeapon([FromUri] string gangerId, Weapon weapon)
        {
            var gangerWeapon = new GangerWeapon()
            {
                Weapon = weapon,
                GangerId = gangerId
            };
            return _weaponFactory.AddGangerWeapon(gangerWeapon);
        }

        [HttpDelete]
        [Route("{gangerId}/weapons/{gangerWeaponId}")]
        public void RemoveGangerWeapon([FromUri] string gangerId, string gangerWeaponId)
        {
            _weaponFactory.RemoveGangerWeapon(gangerWeaponId);
        }
    }
}
