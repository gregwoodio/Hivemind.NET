using Hivemind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Factories
{
    public interface IWeaponFactory
    {
        Weapon GetWeapon(int weaponId);
        IEnumerable<Weapon> GetAllWeapons();
        IEnumerable<GangWeapon> GetGangWeapons(string gangId);
        IEnumerable<GangerWeapon> GetGangerWeapons(string gangerId);
        GangerWeapon AddGangerWeapon(GangerWeapon gangerWeapon);
        void RemoveGangerWeapon(string gangerWeaponId);
        GangWeapon AddGangWeapon(GangWeapon gangWeapon);
        void RemoveGangWeapon(string gangWeaponId);
    }
}
