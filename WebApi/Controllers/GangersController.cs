using Hivemind.Entities;
using Hivemind.Managers;
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
        private IGangerManager _gangerManager;
        private IWeaponManager _weaponFactory;

        public GangersController(IGangerManager gangerManager, IWeaponManager weaponManager)
        {
            if (gangerManager == null)
            {
                throw new ArgumentNullException(nameof(gangerManager));
            }
            if (weaponManager == null)
            {
                throw new ArgumentNullException(nameof(weaponManager));
            }
            _gangerManager = gangerManager;
            _weaponFactory = weaponManager;
        }

        [Authorize]
        [HttpPost]
        public Ganger AddGanger(Ganger ganger)
        {
            return _gangerManager.AddGanger(ganger);
        }

        [Authorize]
        [HttpGet]
        [Route("{gangerId}")]
        public Ganger GetGanger([FromUri] string gangerId)
        {
            return _gangerManager.GetGanger(gangerId);
        }

        [Authorize]
        [HttpPut]
        public Ganger UpdateGanger(Ganger ganger)
        {
            return _gangerManager.UpdateGanger(ganger);
        }

        // weapon routes
        [Authorize]
        [HttpGet]
        [Route("{gangerId}/weapons")]
        public IEnumerable<GangerWeapon> GetWeapons([FromUri] string gangerId)
        {
            return _weaponFactory.GetGangerWeapons(gangerId);
        }

        [Authorize]
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

        [Authorize]
        [HttpDelete]
        [Route("{gangerId}/weapons/{gangerWeaponId}")]
        public void RemoveGangerWeapon([FromUri] string gangerId, string gangerWeaponId)
        {
            _weaponFactory.RemoveGangerWeapon(gangerWeaponId);
        }
    }
}
