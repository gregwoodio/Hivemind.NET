using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Entities
{
    public class GangerWeapon
    {
        public string GangerWeaponId { get; set; }
        public string GangerId { get; set; }
        public Weapon Weapon { get; set; }

        /// <summary>
        /// Cost is set here as an integer, and represents the amount paid for the item.
        /// Some weapons have variable cost, and for purposes of calculating gang rating 
        /// we need the exact number.
        /// </summary>
        public int Cost { get; set; }

        public GangerWeapon()
        {
            Weapon = new Weapon();
        }
    }
}
