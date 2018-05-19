// <copyright file="Weapon.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System;
using Hivemind.Enums;

namespace Hivemind.Entities
{
    /// <summary>
    /// Weapon
    /// </summary>
    public class Weapon
    {
        /// <summary>
        /// Gets or sets the WeaponId
        /// </summary>
        public WeaponEnum WeaponId { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the WeaponType
        /// </summary>
        public WeaponType WeaponType { get; set; }

        /// <summary>
        /// Gets or sets the WeaponAvailability
        /// </summary>
        public WeaponAvailability WeaponAvailability { get; set; }

        /// <summary>
        /// Gets or sets the ShortRange value.
        /// </summary>
        public string ShortRange { get; set; }

        /// <summary>
        /// Gets or sets the LongRange value
        /// </summary>
        public string LongRange { get; set; }

        /// <summary>
        /// Gets or sets the HitShort value
        /// </summary>
        public string HitShort { get; set; }

        /// <summary>
        /// Gets or sets the HitLong value
        /// </summary>
        public string HitLong { get; set; }

        /// <summary>
        /// Gets or sets the Strength
        /// </summary>
        public string Strength { get; set; }

        /// <summary>
        /// Gets or sets the Damage
        /// </summary>
        public string Damage { get; set; }

        /// <summary>
        /// Gets or sets the SaveMod
        /// </summary>
        public string SaveMod { get; set; }

        /// <summary>
        /// Gets or sets the AmmoRoll
        /// </summary>
        public string AmmoRoll { get; set; }

        /// <summary>
        /// Gets or sets the Cost
        /// </summary>
        public string Cost { get; set; }

        /// <summary>
        /// Gets or sets the SpecialRules
        /// </summary>
        public string SpecialRules { get; set; }

        /// <summary>
        /// Gets or sets the effect of using the weapon
        /// </summary>
        public Func<Ganger, Ganger> Effect { get; set; }
    }
}
