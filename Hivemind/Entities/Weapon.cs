using Hivemind.Enums;

namespace Hivemind.Entities
{
    public delegate Ganger WeaponEffect(Ganger ganger);

    public class Weapon
    {
        public WeaponEnum WeaponEnum { get; set; }
        public string Name { get; set; }
        public WeaponType WeaponType { get; set; }
        public WeaponAvailability WeaponAvailability { get; set; }
        public string ShortRange { get; set; }
        public string LongRange { get; set; }
        public string HitShort { get; set; }
        public string HitLong { get; set; }
        public string Strength { get; set; }
        public string Damage { get; set; }
        public string SaveMod { get; set; }
        public string AmmoRoll { get; set; }
        public string Cost { get; set; }
        public string SpecialRules { get; set; }
        public WeaponEffect Effect { get; set; }
    }
}
