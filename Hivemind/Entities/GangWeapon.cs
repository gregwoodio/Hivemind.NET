using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Entities
{
    public class GangWeapon
    {
        public string GangWeaponId { get; set; }
        public string GangId { get; set; }
        public Weapon Weapon { get; set; }

        public GangWeapon()
        {
            Weapon = new Weapon();
        }
    }
}
