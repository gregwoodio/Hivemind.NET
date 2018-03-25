// <copyright file="GangWeapon.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

namespace Hivemind.Entities
{
    /// <summary>
    /// Represents a weapon belonging to a gang, but unassigned to any ganger.
    /// </summary>
    public class GangWeapon
    {
        /// <summary>
        /// Gets or sets the GangWeaponId.
        /// </summary>
        public string GangWeaponId { get; set; }

        /// <summary>
        /// Gets or sets the GangId.
        /// </summary>
        public string GangId { get; set; }

        /// <summary>
        /// Gets or sets the Weapon.
        /// </summary>
        public Weapon Weapon { get; set; }

        /// <summary>
        /// Gets or sets weapon cost (actual amount paid)
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="GangWeapon"/> class.
        /// </summary>
        public GangWeapon()
        {
            Weapon = new Weapon();
        }
    }
}
