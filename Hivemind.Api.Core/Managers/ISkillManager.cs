// <copyright file="ISkillManager.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using Hivemind.Entities;
using Hivemind.Enums;

namespace Hivemind.Managers
{
    /// <summary>
    /// Skill manager interface
    /// </summary>
    public interface ISkillManager
    {
        /// <summary>
        /// Get Skill
        /// </summary>
        /// <param name="skillId">Skill ID</param>
        /// <returns>Skill</returns>
        Skill GetSkill(int skillId);

        /// <summary>
        /// Get random skill by type
        /// </summary>
        /// <param name="type">The skill type</param>
        /// <returns>A random skill</returns>
        Skill GetRandomSkillByType(SkillType type);
    }
}
