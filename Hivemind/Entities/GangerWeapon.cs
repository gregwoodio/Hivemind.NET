// <copyright file="GangerWeapon.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

namespace Hivemind.Entities
{
    /// <summary>
    /// Represents a weapon belonging to a ganger
    /// </summary>
    public class GangerWeapon
    {
        /// <summary>
        /// Gets or sets the GangerWeaponId
        /// </summary>
        public string GangerWeaponId { get; set; }

        /// <summary>
        /// Gets or sets the GangerId
        /// </summary>
        public string GangerId { get; set; }

        /// <summary>
        /// Gets or sets the Weapon
        /// </summary>
        public Weapon Weapon { get; set; }

        /// <summary>
        /// Gets or sets the cost. Cost is set here as an integer, and represents the amount paid for
        /// the item. Some weapons have variable cost, and for purposes of calculating gang rating
        /// we need the exact number.
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GangerWeapon"/> class.
        /// </summary>
        public GangerWeapon()
        {
            Weapon = new Weapon();
        }
    }
}
