using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Exceptions;
using Hivemind.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
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
            var principal = User as ClaimsPrincipal;
            var userId = principal.Claims.FirstOrDefault(claim => claim.Type == "userId");
            if (userId == null)
            {
                throw new HivemindException("User does not have a user ID");
            }
            
            var gangEntity = _gangManager.AddGang(gang);

            _gangManager.AssociateGangToUser(gang.GangId, userId.Value);

            return gangEntity;
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
