// <copyright file="GangTerritory.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

namespace Hivemind.Entities
{
    /// <summary>
    /// Represents a territory belonging to a gang.
    /// </summary>
    public class GangTerritory
    {
        /// <summary>
        /// Gets or sets the GangTerritoryId
        /// </summary>
        public string GangTerritoryId { get; set; }

        /// <summary>
        /// Gets or sets the GangId
        /// </summary>
        public string GangId { get; set; }

        /// <summary>
        /// Gets or sets the Territory
        /// </summary>
        public Territory Territory { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GangTerritory"/> class.
        /// </summary>
        public GangTerritory()
        {
            Territory = new Territory();
        }
    }
}
