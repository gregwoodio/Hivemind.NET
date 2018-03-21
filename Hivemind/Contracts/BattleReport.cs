// <copyright file="BattleReport.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Hivemind.Enums;

namespace Hivemind.Contracts
{
    /// <summary>
    /// Returned from the web console after a match. Should contain all information about how the gangers performed during
    /// the match, as well as which gang was used, whether they won, and the opponent gang rating.
    /// </summary>
    public class BattleReport
    {
        /// <summary>
        /// Gets or sets the gang ID.
        /// </summary>
        public string GangId { get; set; }

        /// <summary>
        /// Gets or sets the gang battle stats.
        /// </summary>
        public IEnumerable<GangerBattleStats> GangBattleStats { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the gang won.
        /// </summary>
        public bool HasWon { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the gang was the attacker.
        /// </summary>
        public bool IsAttacker { get; set; }

        /// <summary>
        /// Gets or sets the opponent gang's rating
        /// </summary>
        public int OpponentGangRating { get; set; }

        /// <summary>
        /// Gets or sets the game type.
        /// </summary>
        public GameType GameType { get; set; }
    }
}
