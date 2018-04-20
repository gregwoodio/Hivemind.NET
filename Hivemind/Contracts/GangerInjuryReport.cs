// <copyright file="GangerInjuryReport.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Hivemind.Entities;

namespace Hivemind.Contracts
{
    /// <summary>
    /// Ganger injury report.
    /// </summary>
    public class GangerInjuryReport
    {
        /// <summary>
        /// Gets or sets the ganger.
        /// </summary>
        public Ganger TheGanger { get; set; }

        /// <summary>
        /// Gets or sets the list of injuries.
        /// </summary>
        public IEnumerable<Injury> Injuries { get; set; }
    }
}
