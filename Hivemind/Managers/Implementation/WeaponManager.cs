using Hivemind.Entities;
using Hivemind.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Managers.Implementation
{
    public class WeaponManager : IWeaponManager
    {
        private WeaponProvider _weaponProvider;

        public WeaponManager(WeaponProvider weaponProvider)
        {
            _weaponProvider = weaponProvider ?? throw new ArgumentNullException(nameof(weaponProvider));
        }

        public Weapon GetWeapon(int weaponId)
        {
            return _weaponProvider.GetById(weaponId);
        }

        public IEnumerable<Weapon> GetAllWeapons()
        {
            return _weaponProvider.GetAllWeapons();
        }

        public GangWeapon AddGangWeapon(GangWeapon gangWeapon)
        {
            return _weaponProvider.AddGangWeapon(gangWeapon);
        }

        public void RemoveGangWeapon(string gangWeaponId)
        {
            _weaponProvider.RemoveGangWeapon(gangWeaponId);
        }

        public IEnumerable<Weapon> GetGangStash(string gangId)
        {
            return _weaponProvider.GetGangStash(gangId);
        }

        public IEnumerable<Weapon> GetGangerWeaponsByGangId(string gangId)
        {
            return _weaponProvider.GetGangerWeaponsByGangId(gangId);
        }

        public GangerWeapon AddGangerWeapon(GangerWeapon gangerWeapon)
        {
            return _weaponProvider.AddGangerWeapon(gangerWeapon);
        }

        public void RemoveGangerWeapon(string gangerWeaponId)
        {
            _weaponProvider.RemoveGangerWeapon(gangerWeaponId);
        }

        public IEnumerable<GangerWeapon> GetGangerWeapons(string gangerId)
        {
            return _weaponProvider.GetGangerWeapons(gangerId);
        }
    }
}
