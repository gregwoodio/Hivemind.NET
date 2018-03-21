// <copyright file="Skill.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using Hivemind.Enums;
using System.Collections.Generic;

namespace Hivemind.Entities
{
#pragma warning disable CS0659 // Type overrides Object.Equals(object o) but does not override Object.GetHashCode()
    /// <summary>
    /// Represents a Skill
    /// </summary>
    public class Skill
    {
        /// <summary>
        /// Gets or sets the SkillId
        /// </summary>
        public int SkillId { get; set; }

        /// <summary>
        /// Gets or sets the Name
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Gets or sets the Description
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Gets or sets the SkillType
        /// </summary>
        public SkillType SkillType { get; set; }

        /// <summary>
        /// Gets or sets the restricted types.
        /// </summary>
        public GangerType[] RestrictedTypes { get; set; }

        /// <summary>
        /// Equality comparison for Skill.
        /// </summary>
        /// <param name="obj">The object being compared to a skill</param>
        /// <returns>True if equal.</returns>
        public override bool Equals(object obj)
        {
            if (obj.GetType() == typeof(Skill))
            {
                return SkillId == ((Skill)obj).SkillId;
            }

            return false;
        }
    }
}
