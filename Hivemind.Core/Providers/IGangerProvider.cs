// <copyright file="IGangerProvider.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Hivemind.Entities;
using Hivemind.Enums;

namespace Hivemind.Providers
{
    /// <summary>
    /// Ganger provider interface
    /// </summary>
    public interface IGangerProvider
    {
        /// <summary>
        /// Get by Ganger ID
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <returns>Ganger</returns>
        Ganger GetByGangerId(string gangerId);

        /// <summary>
        /// Get by Gang ID
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>List of gangers in the gang</returns>
        IEnumerable<Ganger> GetByGangId(string gangId);

        /// <summary>
        /// Add ganger
        /// </summary>
        /// <param name="ganger">Ganger</param>
        /// <returns>Added ganger</returns>
        Ganger AddGanger(Ganger ganger);

        /// <summary>
        /// Get ganger skills
        /// </summary>
        /// <param name="gangId">GangId</param>
        /// <returns>List of GangerSkills</returns>
        IEnumerable<GangerSkill> GetGangerSkills(string gangId);

        /// <summary>
        /// Update ganger
        /// </summary>
        /// <param name="ganger">Ganger</param>
        /// <returns>Updated ganger</returns>
        Ganger UpdateGanger(Ganger ganger);

        /// <summary>
        /// Add ganger skill
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <param name="skillId">Skill ID</param>
        void AddGangerSkill(string gangerId, int skillId);

        /// <summary>
        /// Add injury to ganger
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <param name="injury">Injury ID</param>
        void AddGangerInjury(string gangerId, InjuryEnum injury);

        /// <summary>
        /// Can the ganger learn a skill? Check if advancement ID is valid for ganger.
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <param name="advancementId">Advancement ID</param>
        /// <returns>Whether ganger can learn a skill or not</returns>
        bool CanLearnSkill(string gangerId, string advancementId);

        /// <summary>
        /// Register ganger advancement. Make a ganger eligible for an advance roll.
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <returns>The advancement ID</returns>
        string RegisterGangerAdvancement(string gangerId);

        /// <summary>
        /// Remove ganger advancment. Ganger has advanced.
        /// </summary>
        /// <param name="gangerId">Ganger ID</param>
        /// <param name="advancementId">Advancement ID</param>
        void RemoveGangerAdvancement(string gangerId, string advancementId);
    }
}
