// <copyright file="ITerritoryManager.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Hivemind.Contracts;
using Hivemind.Entities;

namespace Hivemind.Managers
{
    /// <summary>
    /// Territory manager interface
    /// </summary>
    public interface ITerritoryManager
    {
        /// <summary>
        /// Get Territory
        /// </summary>
        /// <param name="territoryId">Territory ID</param>
        /// <returns>Territory</returns>
        Territory GetTerritory(int territoryId);

        /// <summary>
        /// Get all territories
        /// </summary>
        /// <returns>All territories</returns>
        IEnumerable<Territory> GetAllTerritories();

        /// <summary>
        /// Get territories by gang ID
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>Territories owned by a gang</returns>
        IEnumerable<Territory> GetTerritoriesByGangId(string gangId);

        /// <summary>
        /// Get territory effect, representing the actions that occur when working a territory.
        /// </summary>
        /// <param name="territoryId">Territory ID</param>
        /// <returns>Function that makes changes based on territory worked.</returns>
        Func<TerritoryWorkStatus, TerritoryIncomeReport> GetTerritoryEffect(int territoryId);

        /// <summary>
        /// Add gang territory
        /// </summary>
        /// <param name="territory">Gang territory</param>
        /// <returns>Added gang territory</returns>
        GangTerritory AddGangTerritory(GangTerritory territory);

        /// <summary>
        /// Remove gang territory
        /// </summary>
        /// <param name="gangTerritoryId">Gang territory ID</param>
        void RemoveGangTerritory(string gangTerritoryId);
    }
}
