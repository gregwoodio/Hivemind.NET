// <copyright file="WeaponManager.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Hivemind.Entities;
using Hivemind.Providers;

namespace Hivemind.Managers.Implementation
{
    /// <summary>
    /// Weapon manager
    /// </summary>
    public class WeaponManager : IWeaponManager
    {
        private IWeaponProvider _weaponProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="WeaponManager"/> class.
        /// </summary>
        /// <param name="weaponProvider">Weapon provider</param>
        public WeaponManager(IWeaponProvider weaponProvider)
        {
            _weaponProvider = weaponProvider ?? throw new ArgumentNullException(nameof(weaponProvider));
        }

        /// <summary>
        /// Get Weapon
        /// </summary>
        /// <param name="weaponId">The weapon ID</param>
        /// <returns>Requested weapon</returns>
        public Weapon GetWeapon(int weaponId)
        {
            return _weaponProvider.GetById(weaponId);
        }

        /// <summary>
        /// Get all weapons
        /// </summary>
        /// <returns>Weapons</returns>
        public IEnumerable<Weapon> GetAllWeapons()
        {
            return _weaponProvider.GetAllWeapons();
        }

        /// <summary>
        /// Add gang weapon
        /// </summary>
        /// <param name="gangWeapon">Gang weapon</param>
        /// <returns>Added gang weapon</returns>
        public GangWeapon AddGangWeapon(GangWeapon gangWeapon)
        {
            return _weaponProvider.AddGangWeapon(gangWeapon);
        }

        /// <summary>
        /// Remove Gang weapon
        /// </summary>
        /// <param name="gangWeaponId">Gang weapon ID</param>
        public void RemoveGangWeapon(string gangWeaponId)
        {
            _weaponProvider.RemoveGangWeapon(gangWeaponId);
        }

        /// <summary>
        /// Get gang stash (weapons belonging to a gang but unassigned to a ganger)
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>Weapon list</returns>
        public IEnumerable<Weapon> GetGangStash(string gangId)
        {
            return _weaponProvider.GetGangStash(gangId);
        }

        /// <summary>
        /// Get ganger weapons by gang ID
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>Weapons equipped to gangers in specified gang</returns>
        public IEnumerable<Weapon> GetGangerWeaponsByGangId(string gangId)
        {
            return _weaponProvider.GetGangerWeaponsByGangId(gangId);
        }

        /// <summary>
        /// Add ganger weapon (equip weapon to ganger)
        /// </summary>
        /// <param name="gangerWeapon">Ganger weapon</param>
        /// <returns>Added ganger weapon</returns>
        public GangerWeapon AddGangerWeapon(GangerWeapon gangerWeapon)
        {
            return _weaponProvider.AddGangerWeapon(gangerWeapon);
        }

        /// <summary>
        /// Remove ganger weapon (unequip weapon from ganger)
        /// </summary>
        /// <param name="gangerWeaponId">Ganger weapon ID to remove</param>
        public void RemoveGangerWeapon(string gangerWeaponId)
        {
            _weaponProvider.RemoveGangerWeapon(gangerWeaponId);
        }

        /// <summary>
        /// Get ganger weapons (all equipped weapons)
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <returns>Ganger weapons</returns>
        public IEnumerable<GangerWeapon> GetGangerWeapons(string gangerId)
        {
            return _weaponProvider.GetGangerWeapons(gangerId);
        }
    }
}
