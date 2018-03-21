// <copyright file="IGangerManager.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using Hivemind.Entities;
using Hivemind.Enums;

namespace Hivemind.Managers
{
    /// <summary>
    /// IGangerManager
    /// </summary>
    public interface IGangerManager
    {
        /// <summary>
        /// Gets a ganger by ID
        /// </summary>
        /// <param name="id">Ganger Id</param>
        /// <returns>Ganger corresponding to the ID</returns>
        Ganger GetGanger(string id);

        /// <summary>
        /// Create a ganger with default stats for the specified type
        /// </summary>
        /// <param name="name">Ganger name</param>
        /// <param name="type">Ganger type</param>
        /// <returns>The ganger</returns>
        Ganger CreateGanger(string name, GangerType type);

        /// <summary>
        /// Creates a Juve
        /// </summary>
        /// <param name="name">Name</param>
        /// <returns>Ganger</returns>
        Ganger CreateJuve(string name);

        /// <summary>
        /// Create ganger
        /// </summary>
        /// <param name="name">Ganger name</param>
        /// <returns>Ganger</returns>
        Ganger CreateGanger(string name);

        /// <summary>
        /// Create heavy
        /// </summary>
        /// <param name="name">Ganger name</param>
        /// <returns>Ganger</returns>
        Ganger CreateHeavy(string name);

        /// <summary>
        /// Create leader
        /// </summary>
        /// <param name="name">Ganger name</param>
        /// <returns>Ganger</returns>
        Ganger CreateLeader(string name);

        /// <summary>
        /// Update a ganger
        /// </summary>
        /// <param name="ganger">Ganger</param>
        /// <returns>Updated Ganger</returns>
        Ganger UpdateGanger(Ganger ganger);

        /// <summary>
        /// Increase a statistic
        /// </summary>
        /// <param name="ganger">Ganger</param>
        /// <param name="stat">Statistic</param>
        /// <param name="interval">Interval</param>
        /// <returns>Updated ganger</returns>
        Ganger IncreaseStat(Ganger ganger, GangerStatistics stat, int? interval);

        /// <summary>
        /// Add ganger
        /// </summary>
        /// <param name="ganger">Ganger</param>
        /// <returns>Added Ganger</returns>
        Ganger AddGanger(Ganger ganger);

        /// <summary>
        /// Add ganger injury
        /// </summary>
        /// <param name="gangerId">Ganger's ID</param>
        /// <param name="injuryEnum">Injury</param>
        void AddGangerInjury(string gangerId, InjuryEnum injuryEnum);

        /// <summary>
        /// Learn skill
        /// </summary>
        /// <param name="ganger">The ganger</param>
        /// <param name="advancementId">The advancement ID. Used to verify that the
        /// ganger is able to learn a new skill.</param>
        /// <param name="type">Skill type</param>
        /// <returns>Ganger skill</returns>
        GangerSkill LearnSkill(Ganger ganger, string advancementId, SkillType type);

        /// <summary>
        /// Register a ganger for advancement (able to learn a new skill)
        /// </summary>
        /// <param name="gangerId">Ganger Id</param>
        /// <returns>The advancement ID</returns>
        string RegisterGangerAdvancement(string gangerId);
    }
}
