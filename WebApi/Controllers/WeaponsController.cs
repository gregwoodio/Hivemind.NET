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
    [RoutePrefix("api/weapons")]
    public class WeaponsController : ApiController
    {
        private IWeaponFactory _weaponFactory;

        public WeaponsController(IWeaponFactory weaponFactory)
        {
            if (weaponFactory == null)
            {
                throw new ArgumentNullException(nameof(weaponFactory));
            }
            _weaponFactory = weaponFactory;
        }

        [HttpGet]
        [Route("{weaponId}")]
        public Weapon GetWeapon([FromUri] int weaponId)
        {
            return _weaponFactory.GetWeapon(weaponId);
        }

        [HttpGet]
        public IEnumerable<Weapon> GetAllWeapons()
        {
            return _weaponFactory.GetAllWeapons();
        }
    }
}
