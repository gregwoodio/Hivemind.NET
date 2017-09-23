using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Factories;
using System;
using System.Collections.Generic;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi.Controllers
{
    [RoutePrefix("api/gangs")]
    [EnableCors(origins: "http://localhost:4200", headers:"*", methods: "*")]
    public class GangsController : ApiController
    {
        private IGangFactory _gangFactory;
        private IWeaponFactory _weaponFactory;

        public GangsController(IGangFactory gangFactory, IWeaponFactory weaponFactory)
        {
            if (gangFactory == null)
            {
                throw new ArgumentNullException(nameof(gangFactory));
            }
            if (weaponFactory == null)
            {
                throw new ArgumentNullException(nameof(weaponFactory));
            }
            _gangFactory = gangFactory;
            _weaponFactory = weaponFactory;
        }

        [HttpGet]
        [Route("{gangId}")]
        public Gang GetGang([FromUri] string gangId)
        {
            return _gangFactory.GetGang(gangId);
        }

        [HttpPost]
        [Route("{gangName}")]
        public Gang AddGang([FromUri] string gangName, [FromBody] GangHouse house)
        {
            var gang = new Gang()
            {
                Name = gangName,
                GangHouse = house,
            };
            return _gangFactory.AddGang(gang);
        }

        [HttpPut]
        public Gang UpdateGang(Gang gang)
        {
            return _gangFactory.UpdateGang(gang);
        }

        // weapon routes
        [HttpGet]
        [Route("{gangId}/weapons")]
        public IEnumerable<GangWeapon> GetWeapons([FromUri] string gangId)
        {
            return _weaponFactory.GetGangWeapons(gangId);
        }

        [HttpPost]
        [Route("{gangId}/weapons")]
        public GangWeapon AddGangWeapon([FromUri] string gangId, Weapon weapon)
        {
            var gangWeapon = new GangWeapon()
            {
                Weapon = weapon,
                GangId = gangId
            };
            return _weaponFactory.AddGangWeapon(gangWeapon);
        }

        [HttpDelete]
        [Route("{gangId}/weapons/{gangWeaponId}")]
        public void RemoveGangWeapon([FromUri] string gangId, string gangWeaponId)
        {
            _weaponFactory.RemoveGangWeapon(gangWeaponId);
        }
    }
}
