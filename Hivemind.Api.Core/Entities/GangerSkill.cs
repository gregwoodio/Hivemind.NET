// <copyright file="GangerSkill.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

namespace Hivemind.Entities
{
    /// <summary>
    /// GangerSkill represents a skill belonging to a ganger.
    /// </summary>
    public class GangerSkill
    {
        /// <summary>
        /// Gets or sets the ID of the skill.
        /// </summary>
        public int SkillId { get; set; }

        /// <summary>
        /// Gets or sets the ID of the ganger.
        /// </summary>
        public string GangerId { get; set; }
    }
}
