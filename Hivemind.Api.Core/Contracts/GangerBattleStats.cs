// <copyright file="GangerBattleStats.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

namespace Hivemind.Contracts
{
    /// <summary>
    /// GangerBattleStats
    /// </summary>
    public class GangerBattleStats
    {
        /// <summary>
        /// Gets or sets the ganger ID
        /// </summary>
        public string GangerId { get; set; }

        /// <summary>
        /// Gets or sets the ganger's kills.
        /// </summary>
        public int Kills { get; set; }

        /// <summary>
        /// Gets or sets the ganger's acheived objectives.
        /// </summary>
        public int Objectives { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the ganger was down in the battle.
        /// </summary>
        public bool Down { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the ganger was out of action in the battle.
        /// </summary>
        public bool OutOfAction { get; set; }
    }
}
