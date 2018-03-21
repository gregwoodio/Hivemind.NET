// <copyright file="Gang.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Hivemind.Enums;

namespace Hivemind.Entities
{
    /// <summary>
    /// Represents a player's Gang.
    /// </summary>
    public class Gang
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Gang"/> class.
        /// </summary>
        public Gang()
        {
            Credits = 1000;
            Gangers = new List<Ganger>();
            Territories = new List<Territory>();
        }

        /// <summary>
        /// Gets or sets the GangId
        /// </summary>
        public string GangId { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Credits
        /// </summary>
        public int Credits { get; set; }

        /// <summary>
        /// Gets or sets the GangHouse
        /// </summary>
        public GangHouse GangHouse { get; set; }

        /// <summary>
        /// Gets or sets the Gangers
        /// </summary>
        public IEnumerable<Ganger> Gangers { get; set; }

        /// <summary>
        /// Gets or sets the Territories
        /// </summary>
        public IEnumerable<Territory> Territories { get; set; }

        /// <summary>
        /// Gets the GangRating
        /// </summary>
        public int GangRating
        {
            get
            {
                int rating = 0;
                foreach (var ganger in Gangers)
                {
                    rating += ganger.Cost + ganger.Experience;
                }

                return rating;
            }
        }
    }
}