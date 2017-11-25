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
    [RoutePrefix("api/weapons")]
    public class WeaponsController : ApiController
    {
        private IWeaponManager _weaponManager;

        public WeaponsController(IWeaponManager weaponManager)
        {
            if (weaponManager == null)
            {
                throw new ArgumentNullException(nameof(weaponManager));
            }
            _weaponManager = weaponManager;
        }

        [Authorize]
        [HttpGet]
        [Route("{weaponId}")]
        public Weapon GetWeapon([FromUri] int weaponId)
        {
            return _weaponManager.GetWeapon(weaponId);
        }

        [Authorize]
        [HttpGet]
        public IEnumerable<Weapon> GetAllWeapons()
        {
            return _weaponManager.GetAllWeapons();
        }
    }
}
