// <copyright file="IGameService.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Hivemind.Contracts;
using Hivemind.Entities;

namespace Hivemind.Services
{
    /// <summary>
    /// Game service
    /// </summary>
    public interface IGameService
    {
        /// <summary>
        /// Process pre game events, such as Old Battle Wound, Spore Sickness, etc. Anything that needs to happen 
        /// before the battle should go here.
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>Pre game report</returns>
        PreGameReport ProcessPreGame(string gangId);

        /// <summary>
        /// Process post game. Determine injuries, experience, income.
        /// </summary>
        /// <param name="battleReport">Battle report</param>
        /// <returns>Post game report</returns>
        PostGameReport ProcessPostGame(BattleReport battleReport);

        /// <summary>
        /// Skill up gangers. Requires user interaction, so happens after post game is processed.
        /// </summary>
        /// <param name="skillUpRequest">Skill up request</param>
        /// <returns>List of ganger skills.</returns>
        IEnumerable<GangerSkill> SkillUpGangers(GangSkillUpRequest skillUpRequest);
    }
}
