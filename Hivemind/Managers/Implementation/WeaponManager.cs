using Hivemind.Entities;
using Hivemind.Providers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Managers
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

        public IEnumerable<GangWeapon> GetGangWeapons(string gangId)
        {
            return _weaponProvider.GetGangWeapons(gangId);
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
