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

        public GangerWeapon()
        {
            Weapon = new Weapon();
        }
    }
}
