// <copyright file="GangerManager.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System;
using System.Linq;
using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Exceptions;
using Hivemind.Providers;
using Hivemind.Utilities;

namespace Hivemind.Managers.Implementation
{
    /// <summary>
    /// Ganger manager
    /// </summary>
    public class GangerManager : IGangerManager
    {
        private IGangerProvider _gangerProvider;
        private ISkillManager _skillManager;
        private IDiceRoller _diceRoller;

        /// <summary>
        /// Initializes a new instance of the <see cref="GangerManager"/> class.
        /// </summary>
        /// <param name="gangerProvider">Ganger provider</param>
        /// <param name="skillManager">Skill manager</param>
        /// <param name="diceRoller">Dice roller</param>
        public GangerManager(IGangerProvider gangerProvider, ISkillManager skillManager, IDiceRoller diceRoller)
        {
            _gangerProvider = gangerProvider ?? throw new ArgumentNullException(nameof(gangerProvider));
            _skillManager = skillManager ?? throw new ArgumentNullException(nameof(skillManager));
            _diceRoller = diceRoller ?? throw new ArgumentNullException(nameof(diceRoller));
        }

        /// <summary>
        /// Gets a ganger by ID
        /// </summary>
        /// <param name="id">Ganger Id</param>
        /// <returns>Ganger corresponding to the ID</returns>
        public Ganger GetGanger(string id)
        {
            return _gangerProvider.GetByGangerId(id);
        }

        /// <summary>
        /// Update a ganger
        /// </summary>
        /// <param name="ganger">Ganger</param>
        /// <returns>Updated Ganger</returns>
        public Ganger UpdateGanger(Ganger ganger)
        {
            return _gangerProvider.UpdateGanger(ganger);
        }

        /// <summary>
        /// Create a ganger with default stats for the specified type
        /// </summary>
        /// <param name="name">Ganger name</param>
        /// <param name="type">Ganger type</param>
        /// <returns>The ganger</returns>
        public Ganger CreateGanger(string name, GangerType type)
        {
            switch (type)
            {
                case GangerType.Leader:
                    return CreateLeader(name);
                case GangerType.Heavy:
                    return CreateHeavy(name);
                case GangerType.Ganger:
                    return CreateGanger(name);
                case GangerType.Juve:
                    return CreateJuve(name);
                default:
                    throw new HivemindException($"Invalid GangerType provided: ${type}");
            }
        }

        /// <summary>
        /// Creates a Juve
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Ganger</returns>
        public Ganger CreateJuve(string name)
        {
            return new Ganger()
            {
                Name = name,
                GangerType = GangerType.Juve,
                Move = 4,
                WeaponSkill = 2,
                BallisticSkill = 2,
                Strength = 3,
                Toughness = 3,
                Wounds = 1,
                Initiative = 3,
                Attack = 1,
                Leadership = 6,
                Cost = 25,
                Experience = 0,
                Title = GangerTitle.GreenJuve,
                Active = true,
            };
        }

        /// <summary>
        /// Create ganger
        /// </summary>
        /// <param name="name">Ganger name</param>
        /// <returns>Ganger</returns>
        public Ganger CreateGanger(string name)
        {
            return new Ganger()
            {
                Name = name,
                GangerType = GangerType.Ganger,
                Move = 4,
                WeaponSkill = 3,
                BallisticSkill = 3,
                Strength = 3,
                Toughness = 3,
                Wounds = 1,
                Initiative = 3,
                Attack = 1,
                Leadership = 7,
                Cost = 50,
                Experience = 20 + _diceRoller.RollDie(),
                Title = GangerTitle.NewGanger,
                Active = true,
            };
        }

        /// <summary>
        /// Create heavy
        /// </summary>
        /// <param name="name">Ganger name</param>
        /// <returns>Ganger</returns>
        public Ganger CreateHeavy(string name)
        {
            return new Ganger()
            {
                Name = name,
                GangerType = GangerType.Heavy,
                Move = 4,
                WeaponSkill = 3,
                BallisticSkill = 3,
                Strength = 3,
                Toughness = 3,
                Wounds = 1,
                Initiative = 3,
                Attack = 1,
                Leadership = 7,
                Cost = 60,
                Experience = 60 + _diceRoller.RollDie(),
                Title = GangerTitle.GangChampion,
                Active = true,
            };
        }

        /// <summary>
        /// Create leader
        /// </summary>
        /// <param name="name">Ganger name</param>
        /// <returns>Ganger</returns>
        public Ganger CreateLeader(string name)
        {
            return new Ganger()
            {
                Name = name,
                GangerType = GangerType.Leader,
                Move = 4,
                WeaponSkill = 4,
                BallisticSkill = 4,
                Strength = 3,
                Toughness = 3,
                Wounds = 1,
                Initiative = 4,
                Attack = 1,
                Leadership = 8,
                Cost = 120,
                Experience = 60 + _diceRoller.RollDie(),
                Title = GangerTitle.GangChampion,
                Active = true,
            };
        }

        /// <summary>
        /// Increase a statistic
        /// </summary>
        /// <param name="ganger">Ganger</param>
        /// <param name="stat">Statistic</param>
        /// <param name="interval">Interval</param>
        /// <returns>Updated ganger</returns>
        public Ganger IncreaseStat(Ganger ganger, GangerStatistics stat, int interval = 1)
        {
            switch (stat)
            {
                case GangerStatistics.Move:
                    ganger.Move += interval;
                    break;
                case GangerStatistics.WeaponSkill:
                    ganger.WeaponSkill += interval;
                    break;
                case GangerStatistics.BallisticSkill:
                    ganger.BallisticSkill += interval;
                    break;
                case GangerStatistics.Strength:
                    ganger.Strength += interval;
                    break;
                case GangerStatistics.Toughness:
                    ganger.Toughness += interval;
                    break;
                case GangerStatistics.Attack:
                    ganger.Attack += interval;
                    break;
                case GangerStatistics.Wounds:
                    ganger.Wounds += interval;
                    break;
                case GangerStatistics.Initiative:
                    ganger.Initiative += interval;
                    break;
                case GangerStatistics.Leadership:
                    ganger.Leadership += interval;
                    break;
                default:
                    break;
            }

            UpdateGanger(ganger);
            return ganger;
        }

        /// <summary>
        /// Add ganger
        /// </summary>
        /// <param name="ganger">Ganger</param>
        /// <returns>Added Ganger</returns>
        public Ganger AddGanger(Ganger ganger)
        {
            var gangerWithStats = CreateGanger(ganger.Name, ganger.GangerType);
            gangerWithStats.GangId = ganger.GangId;
            return _gangerProvider.AddGanger(gangerWithStats);
        }

        /// <summary>
        /// Add ganger injury
        /// </summary>
        /// <param name="gangerId">Ganger's ID</param>
        /// <param name="injury">Injury</param>
        public void AddGangerInjury(string gangerId, InjuryEnum injury)
        {
            _gangerProvider.AddGangerInjury(gangerId, injury);
        }

        /// <summary>
        /// Learn skill
        /// </summary>
        /// <param name="ganger">The ganger</param>
        /// <param name="advancementId">The advancement ID. Used to verify that the
        /// ganger is able to learn a new skill.</param>
        /// <param name="type">Skill type</param>
        /// <returns>Ganger skill</returns>
        public GangerSkill LearnSkill(Ganger ganger, string advancementId, SkillType type)
        {
            if (!_gangerProvider.CanLearnSkill(ganger.GangerId, advancementId))
            {
                throw new HivemindException("Invalid advancement ID.");
            }

            // TODO: If the ganger knows all the skills of that category, we return for now.
            if (ganger.Skills.Where(s => s.SkillType == type).Count() >= 6)
            {
                return null;
            }

            var skill = _skillManager.GetRandomSkillByType(type);
            while (ganger.Skills.Contains(skill))
            {
                skill = _skillManager.GetRandomSkillByType(type);
            }

            _gangerProvider.AddGangerSkill(ganger.GangerId, skill.SkillId);

            _gangerProvider.RemoveGangerAdvancement(ganger.GangerId, advancementId);

            var skills = ganger.Skills.ToList();
            skills.Add(skill);
            ganger.Skills = skills;

            return new GangerSkill
            {
                SkillId = skill.SkillId,
                GangerId = ganger.GangerId,
            };
        }

        /// <summary>
        /// Register a ganger for advancement (able to learn a new skill)
        /// </summary>
        /// <param name="gangerId">Ganger Id</param>
        /// <returns>The advancement ID</returns>
        public string RegisterGangerAdvancement(string gangerId)
        {
            return _gangerProvider.RegisterGangerAdvancement(gangerId);
        }
    }
}
