using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Managers;
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
        private IGangManager _gangManager;
        private IWeaponManager _weaponManager;

        public GangsController(IGangManager gangManager, IWeaponManager weaponManager)
        {
            if (gangManager == null)
            {
                throw new ArgumentNullException(nameof(gangManager));
            }
            if (weaponManager == null)
            {
                throw new ArgumentNullException(nameof(weaponManager));
            }
            _gangManager = gangManager;
            _weaponManager = weaponManager;
        }

        [HttpGet]
        [Route("{gangId}")]
        public Gang GetGang([FromUri] string gangId)
        {
            return _gangManager.GetGang(gangId);
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
            return _gangManager.AddGang(gang);
        }

        [HttpPut]
        public Gang UpdateGang(Gang gang)
        {
            return _gangManager.UpdateGang(gang);
        }

        // weapon routes
        [HttpGet]
        [Route("{gangId}/weapons")]
        public IEnumerable<GangWeapon> GetWeapons([FromUri] string gangId)
        {
            return _weaponManager.GetGangWeapons(gangId);
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
            return _weaponManager.AddGangWeapon(gangWeapon);
        }

        [HttpDelete]
        [Route("{gangId}/weapons/{gangWeaponId}")]
        public void RemoveGangWeapon([FromUri] string gangId, string gangWeaponId)
        {
            _weaponManager.RemoveGangWeapon(gangWeaponId);
        }
    }
}
