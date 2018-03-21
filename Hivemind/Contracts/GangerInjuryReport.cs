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

        /// <summary>
        /// Gets the injury reports title.
        /// </summary>
        /// <returns>A string listing the ganger's injury names.</returns>
        public string GetTitle()
        {
            var title = $"{TheGanger.Name} has been injured: ";
            foreach (var injury in Injuries)
            {
                title += $"\n{injury.Name}";
            }

            return title;
        }

        /// <summary>
        /// Gets the injury report description
        /// </summary>
        /// <returns>A string listing the ganger's injury descriptions.</returns>
        public string GetDescription()
        {
            var description = string.Empty;
            foreach (var injury in Injuries)
            {
                description += $"{injury.Description}\n";
            }

            return description;
        }
    }
}
