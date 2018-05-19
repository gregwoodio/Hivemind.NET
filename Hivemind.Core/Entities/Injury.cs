// <copyright file="Injury.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System;
using Hivemind.Enums;

namespace Hivemind.Entities
{
    /// <summary>
    /// Represents an Injury
    /// </summary>
    public class Injury
    {
        /// <summary>
        /// Gets or sets the InjuryId,
        /// </summary>
        public InjuryEnum InjuryId { get; set; }

        /// <summary>
        /// Gets or sets the Name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the function representing the injury's effect on a ganger.
        /// </summary>
        public Func<Ganger, Ganger> InjuryEffect { get; set; }
    }
}
