using Hivemind.Entities;
using Hivemind.Managers;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;

namespace WebApi.Controllers
{
    [Route("api/weapons")]
    public class WeaponsController : Controller
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
        public Weapon GetWeapon([FromRoute] int weaponId)
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
