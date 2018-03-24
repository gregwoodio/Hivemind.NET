// <copyright file="IWeaponManager.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Hivemind.Entities;

namespace Hivemind.Managers
{
    /// <summary>
    /// Weapon manager interface
    /// </summary>
    public interface IWeaponManager
    {
        /// <summary>
        /// Get Weapon
        /// </summary>
        /// <param name="weaponId">The weapon ID</param>
        /// <returns>Requested weapon</returns>
        Weapon GetWeapon(int weaponId);

        /// <summary>
        /// Get all weapons
        /// </summary>
        /// <returns>Weapons</returns>
        IEnumerable<Weapon> GetAllWeapons();

        /// <summary>
        /// Get gang stash (weapons belonging to a gang but unassigned to a ganger)
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>Weapon list</returns>
        IEnumerable<Weapon> GetGangStash(string gangId);

        /// <summary>
        /// Get ganger weapons by gang ID
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>Weapons equipped to gangers in specified gang</returns>
        IEnumerable<Weapon> GetGangerWeaponsByGangId(string gangId);

        /// <summary>
        /// Get ganger weapons (all equipped weapons)
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <returns>Ganger weapons</returns>
        IEnumerable<GangerWeapon> GetGangerWeapons(string gangerId);

        /// <summary>
        /// Add ganger weapon (equip weapon to ganger)
        /// </summary>
        /// <param name="gangerWeapon">Ganger weapon</param>
        /// <returns>Added ganger weapon</returns>
        GangerWeapon AddGangerWeapon(GangerWeapon gangerWeapon);

        /// <summary>
        /// Remove ganger weapon (unequip weapon from ganger)
        /// </summary>
        /// <param name="gangerWeaponId">Ganger weapon ID to remove</param>
        void RemoveGangerWeapon(string gangerWeaponId);

        /// <summary>
        /// Add gang weapon
        /// </summary>
        /// <param name="gangWeapon">Gang weapon</param>
        /// <returns>Added gang weapon</returns>
        GangWeapon AddGangWeapon(GangWeapon gangWeapon);

        /// <summary>
        /// Remove Gang weapon
        /// </summary>
        /// <param name="gangWeaponId">Gang weapon ID</param>
        void RemoveGangWeapon(string gangWeaponId);
    }
}
