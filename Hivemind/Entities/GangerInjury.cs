// <copyright file="GangerInjury.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

namespace Hivemind.Entities
{
    /// <summary>
    /// Represents a ganger's injury.
    /// </summary>
    public class GangerInjury
    {
        /// <summary>
        /// Gets or sets the GangerId
        /// </summary>
        public string GangerId { get; set; }

        /// <summary>
        /// Gets or sets the Injury
        /// </summary>
        public Injury Injury { get; set; }
    }
}
