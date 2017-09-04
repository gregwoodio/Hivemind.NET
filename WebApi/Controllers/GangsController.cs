using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Factories;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("api/gangs")]
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
