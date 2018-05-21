// <copyright file="ITerritoryProvider.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Hivemind.Entities;
using Hivemind.Enums;

namespace Hivemind.Providers
{
    /// <summary>
    /// Territory provider interface
    /// </summary>
    public interface ITerritoryProvider
    {
        /// <summary>
        /// Get all territories
        /// </summary>
        /// <returns>All territories</returns>
        IEnumerable<Territory> GetAllTerritories();

        /// <summary>
        /// Get territory by ID
        /// </summary>
        /// <param name="territory">Territory ID</param>
        /// <returns>Territory</returns>
        Territory GetTerritoryById(TerritoryEnum territory);

        /// <summary>
        /// Get Territory by ID
        /// </summary>
        /// <param name="territoryId">Territory ID</param>
        /// <returns>Territory</returns>
        Territory GetTerritoryById(int territoryId);

        /// <summary>
        /// Get territory by gang ID
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>List of territories</returns>
        IEnumerable<Territory> GetTerritoryByGangId(string gangId);

        /// <summary>
        /// Add gang territory
        /// </summary>
        /// <param name="gangTerritory">Gang territory</param>
        /// <returns>Added GangTerritory</returns>
        GangTerritory AddGangTerritory(GangTerritory gangTerritory);

        /// <summary>
        /// Remove gang territory
        /// </summary>
        /// <param name="gangTerritoryId">Gang territory ID</param>
        void RemoveGangTerritory(string gangTerritoryId);
    }
}
