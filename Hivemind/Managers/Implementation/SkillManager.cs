// <copyright file="SkillManager.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System;
using System.Linq;
using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Providers;
using Hivemind.Utilities;

namespace Hivemind.Managers.Implementation
{
    /// <summary>
    /// Skill manager
    /// </summary>
    public class SkillManager : ISkillManager
    {
        private ISkillProvider _skillProvider;

        /// <summary>
        /// Initializes a new instance of the <see cref="SkillManager"/> class.
        /// </summary>
        /// <param name="skillProvider">Skill provider</param>
        public SkillManager(ISkillProvider skillProvider)
        {
            _skillProvider = skillProvider ?? throw new ArgumentNullException(nameof(skillProvider));
        }

        /// <summary>
        /// Get Skill
        /// </summary>
        /// <param name="skillId">Skill ID</param>
        /// <returns>Skill</returns>
        public Skill GetSkill(int skillId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Get random skill by type
        /// </summary>
        /// <param name="type">The skill type</param>
        /// <returns>A random skill</returns>
        public Skill GetRandomSkillByType(SkillType type)
        {
            var skills = _skillProvider.GetSkillsByType(type).ToArray();

            return skills[DiceRoller.RollDie() - 1];
        }
    }
}
