// <copyright file="Territory.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System;
using Hivemind.Contracts;

namespace Hivemind.Entities
{
    /// <summary>
    /// Territory
    /// </summary>
    public class Territory : IComparable
    {
        /// <summary>
        /// Gets or sets the TerritoryId
        /// </summary>
        public int TerritoryId { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the Income
        /// </summary>
        public string Income { get; set; }

        /// <summary>
        /// Gets or sets the WorkTerritory Func, which represents the action and events occurring
        /// when a territory is worked.
        /// </summary>
        public Func<TerritoryWorkStatus, TerritoryIncomeReport> WorkTerritory { get; set; }

        /// <summary>
        /// Territories with higher IDs are generally better, so we should sort by descending ID.
        /// </summary>
        /// <param name="obj">The object being compared to a territory</param>
        /// <returns>Int representing higher or lower precedence.</returns>
        public int CompareTo(object obj)
        {
            if (obj.GetType() == typeof(Territory))
            {
                return TerritoryId - ((Territory)obj).TerritoryId;
            }

            throw new ArgumentException();
        }
    }
}
