using Hivemind.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Entities
{
    public class Ganger
    {
        public string GangerId { get; set; }
        public string GangId { get; set; }
        public string Name { get; set; }
        public GangerType GangerType { get; set; }
        public int Move
        {
            get => _move;
            set => _move = MinimumValues(value);
        }
        public int WeaponSkill
        {
            get => _weaponSkill;
            set => _weaponSkill = MinimumValues(value);
        }
        public int BallisticSkill
        {
            get => _ballisticSkill;
            set => _ballisticSkill = MinimumValues(value);
        }
        public int Strength
        {
            get => _strength;
            set => _strength = MinimumValues(value);
        }
        public int Toughness
        {
            get => _toughness;
            set => _toughness = MinimumValues(value);
        }
        public int Wounds
        {
            get => _wounds;
            set => _wounds = MinimumValues(value);
        }
        public int Initiative
        {
            get => _initiative;
            set => _initiative = MinimumValues(value);
        }
        public int Attack
        {
            get => _attack;
            set => _attack = MinimumValues(value);
        }
        public int Leadership
        {
            get => _leadership;
            set => _leadership = MinimumValues(value);
        }
        public int Experience { get; set; }
        public int Cost { get; set; }
        public long Skills { get; set; }
        public long Injuries { get; set; }
        public bool Active { get; set; }
        public GangerTitle Title { get; set; }

        // injury properties
        public bool IsOneEyed { get; set; }
        public bool IsDeafened { get; set; }
        public bool IsOneHanded { get; set; }
        public int RightHandFingers { get; set; }
        public int LeftHandFingers { get; set; }
        public bool HasHorribleScars { get; set; }
        public bool HasImpressiveScars { get; set; }
        public bool HasHeadWound { get; set; }
        public bool HasOldBattleWound { get; set; }
        public bool IsCaptured { get; set; }
        public bool HasBitterEnmity { get; set; }
        public bool HasSporeSickness { get; set; }

        // private stats fields
        private int _move;
        private int _weaponSkill;
        private int _ballisticSkill;
        private int _strength;
        private int _toughness;
        private int _wounds;
        private int _initiative;
        private int _attack;
        private int _leadership;

        public Ganger()
        {
            IsOneEyed = false;
            IsDeafened = false;
            IsOneHanded = false;
            RightHandFingers = 5;
            LeftHandFingers = 5;
            HasHorribleScars = false;
            HasImpressiveScars = false;
            HasHeadWound = false;
            HasOldBattleWound = false;
            IsCaptured = false;
            HasBitterEnmity = false;
            HasSporeSickness = false;
        }

        public Ganger(Ganger ganger)
        {
            Move = ganger.Move;
            WeaponSkill = ganger.WeaponSkill;
            BallisticSkill = ganger.BallisticSkill;
            Strength = ganger.Strength;
            Toughness = ganger.Toughness;
            Wounds = ganger.Wounds;
            Initiative = ganger.Initiative;
            Attack = ganger.Attack;
            Leadership = ganger.Leadership;
            IsOneEyed = ganger.IsOneEyed;
            IsDeafened = ganger.IsDeafened;
            IsOneHanded = ganger.IsOneHanded;
            RightHandFingers = ganger.RightHandFingers;
            LeftHandFingers = ganger.LeftHandFingers;
            HasHorribleScars = ganger.HasHorribleScars;
            HasImpressiveScars = ganger.HasImpressiveScars;
            HasHeadWound = ganger.HasHeadWound;
            HasOldBattleWound = ganger.HasOldBattleWound;
            IsCaptured = ganger.IsCaptured;
            HasBitterEnmity = ganger.HasBitterEnmity;
            HasSporeSickness = ganger.HasSporeSickness;
        }

        private int MinimumValues(int value)
        {
            if (value < 1)
            {
                value = 1;
            }
            return value;
        }
    }
}
