// <copyright file="IStoreService.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Hivemind.Entities;

namespace Hivemind.Services
{
    /// <summary>
    /// Interface for Store Service
    /// </summary>
    public interface IStoreService
    {
        /// <summary>
        /// Get all the commonly available equipment
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>List of common equipment</returns>
        IEnumerable<GangWeapon> GetCommonEquipment(string gangId);

        /// <summary>
        /// Gets all the rare equipment currently available
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>List of rare equipment</returns>
        IEnumerable<GangWeapon> GetRareEquipment(string gangId);

        /// <summary>
        /// Purchase equipment
        /// </summary>
        /// <param name="equipment">GangWeapon to purchase</param>
        void BuyEquipment(GangWeapon equipment);

        /// <summary>
        /// Purchase multiple equipment items
        /// </summary>
        /// <param name="equipment">GangWeapons to purchase</param>
        void BuyEquipment(IEnumerable<GangWeapon> equipment);
    }
}
