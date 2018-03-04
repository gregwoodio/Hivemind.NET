using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Managers;
using System;
using System.Collections.Generic;
using System.Web.Http;

namespace WebApi.Controllers
{
    [RoutePrefix("api/gangs")]
    //[EnableCors(origins: "http://localhost:4200", headers:"*", methods: "*")]
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
        [Authorize]
        [HttpGet]
        [Route("{gangId}")]
        public Gang GetGang([FromUri] string gangId)
        {
            return _gangManager.GetGang(gangId);
        }

        [Authorize]
        [HttpPost]
        [Route("")]
        public Gang AddGang([FromBody] Gang gang)
        {
            // TODO: validate request
            // TODO: Assign gang to principal
            return _gangManager.AddGang(gang);
        }

        [Authorize]
        [HttpPut]
        public Gang UpdateGang(Gang gang)
        {
            // TODO: validate request
            return _gangManager.UpdateGang(gang);
        }

        // weapon routes
        [Authorize]
        [HttpGet]
        [Route("{gangId}/weapons")]
        public IEnumerable<GangWeapon> GetWeapons([FromUri] string gangId)
        {
            return _weaponManager.GetGangWeapons(gangId);
        }

        [Authorize]
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

        [Authorize]
        [HttpDelete]
        [Route("{gangId}/weapons/{gangWeaponId}")]
        public void RemoveGangWeapon([FromUri] string gangId, string gangWeaponId)
        {
            _weaponManager.RemoveGangWeapon(gangWeaponId);
        }
    }
}
