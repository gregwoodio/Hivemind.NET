// <copyright file="Ganger.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Hivemind.Enums;
using Hivemind.Exceptions;

namespace Hivemind.Entities
{
    /// <summary>
    /// Represents a Ganger.
    /// </summary>
    public class Ganger
    {
        /// <summary>
        /// Gets or sets the GangerId
        /// </summary>
        public string GangerId { get; set; }

        /// <summary>
        /// Gets or sets the GangId
        /// </summary>
        public string GangId { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the GangerType
        /// </summary>
        public GangerType GangerType { get; set; }

        /// <summary>
        /// Gets or sets the Move value
        /// </summary>
        public int Move
        {
            get => _move;
            set => _move = MinimumValues(value);
        }

        /// <summary>
        /// Gets or sets the WeaponSkill value
        /// </summary>
        public int WeaponSkill
        {
            get => _weaponSkill;
            set => _weaponSkill = MinimumValues(value);
        }

        /// <summary>
        /// Gets or sets the BallisticSkill value
        /// </summary>
        public int BallisticSkill
        {
            get => _ballisticSkill;
            set => _ballisticSkill = MinimumValues(value);
        }

        /// <summary>
        /// Gets or sets the Strength value
        /// </summary>
        public int Strength
        {
            get => _strength;
            set => _strength = MinimumValues(value);
        }

        /// <summary>
        /// Gets or sets the Toughness value
        /// </summary>
        public int Toughness
        {
            get => _toughness;
            set => _toughness = MinimumValues(value);
        }

        /// <summary>
        /// Gets or sets the Wounds value
        /// </summary>
        public int Wounds
        {
            get => _wounds;
            set => _wounds = MinimumValues(value);
        }

        /// <summary>
        /// Gets or sets the Initiative value
        /// </summary>
        public int Initiative
        {
            get => _initiative;
            set => _initiative = MinimumValues(value);
        }

        /// <summary>
        /// Gets or sets the Attack value
        /// </summary>
        public int Attack
        {
            get => _attack;
            set => _attack = MinimumValues(value);
        }

        /// <summary>
        /// Gets or sets the Leadership value
        /// </summary>
        public int Leadership
        {
            get => _leadership;
            set => _leadership = MinimumValues(value);
        }

        /// <summary>
        /// Gets or sets the Experience value
        /// </summary>
        public int Experience { get; set; }

        /// <summary>
        /// Gets or sets the Cost value
        /// </summary>
        public int Cost { get; set; }

        /// <summary>
        /// Gets or sets the list of the ganger's skills.
        /// </summary>
        public IEnumerable<Skill> Skills { get; set; }

        /// <summary>
        /// Gets or sets the list of the ganger's injuries.
        /// </summary>
        public IEnumerable<Injury> Injuries { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the ganger is active. Active represents whether the Ganger is an active
        /// member of the gang, or is retired/dead.
        /// </summary>
        public bool Active { get; set; }

        /// <summary>
        /// Gets or sets the Ganger's Title.
        /// </summary>
        public GangerTitle Title { get; set; }

        /// <summary>
        /// Gets or sets the ganger's weapons.
        /// </summary>
        public IEnumerable<Weapon> Weapons { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the ganger is enabled for the upcoming game.
        /// </summary>
        public bool IsEnabled { get; set; }

        #region injury properties

        /// <summary>
        /// Gets or sets a value indicating whether the ganger has one eye.
        /// </summary>
        public bool IsOneEyed { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is deafened.
        /// </summary>
        public bool IsDeafened { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether is one handed.
        /// </summary>
        public bool IsOneHanded { get; set; }

        /// <summary>
        /// Gets or sets the number of fingers on the ganger's right hand.
        /// </summary>
        public int RightHandFingers { get; set; }

        /// <summary>
        /// Gets or sets the number of fingers on the ganger's left hand.
        /// </summary>
        public int LeftHandFingers { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the ganger has horrible scars.
        /// </summary>
        public bool HasHorribleScars { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether has impressive scars.
        /// </summary>
        public bool HasImpressiveScars { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether has a head wound.
        /// </summary>
        public bool HasHeadWound { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether has an old battle wound.
        /// </summary>
        public bool HasOldBattleWound { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the ganger is captured.
        /// </summary>
        public bool IsCaptured { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the ganger has bitter enmity.
        /// </summary>
        public bool HasBitterEnmity { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the ganger has spore sickness.
        /// </summary>
        public bool HasSporeSickness { get; set; }

        /// <summary>
        /// Gets or sets a value indicating whether the ganger has a flesh wound.
        /// </summary>
        public bool HasFleshWound { get; set; }
        #endregion

        /// <summary>
        /// Initializes a new instance of the <see cref="Ganger"/> class.
        /// </summary>
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
            Weapons = new Weapon[0];
            Skills = new Skill[0];
            Injuries = new Injury[0];
            Active = true;
            IsEnabled = true;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Ganger"/> class.
        /// </summary>
        /// <param name="ganger">The ganger from which to copy parameters.</param>
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
            Weapons = ganger.Weapons;
            Skills = ganger.Skills;
            Injuries = ganger.Injuries;
        }

        /// <summary>
        /// Calculates the cost of a ganger.
        /// </summary>
        public void GetCost()
        {
            Cost = 0;
            switch (GangerType)
            {
                case GangerType.Juve:
                    Cost += 25;
                    break;
                case GangerType.Ganger:
                    Cost += 50;
                    break;
                case GangerType.Heavy:
                    Cost += 60;
                    break;
                case GangerType.Leader:
                    Cost += 120;
                    break;
                default:
                    throw new HivemindException("Invalid ganger type specified.");
            }

            foreach (var weapon in Weapons)
            {
                if (int.TryParse(weapon.Cost, out int weaponCost))
                {
                    Cost += weaponCost;
                }
            }
        }

        #region private stats fields
        private int _move;
        private int _weaponSkill;
        private int _ballisticSkill;
        private int _strength;
        private int _toughness;
        private int _wounds;
        private int _initiative;
        private int _attack;
        private int _leadership;
        #endregion

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
