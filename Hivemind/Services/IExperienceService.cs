// <copyright file="IExperienceService.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System.Collections.Generic;
using Hivemind.Contracts;
using Hivemind.Entities;
using Hivemind.Enums;

namespace Hivemind.Services
{
    /// <summary>
    /// Experience Service
    /// </summary>
    public interface IExperienceService
    {
        /// <summary>
        /// Determine experience gains for a gang after a battle.
        /// </summary>
        /// <param name="battleReport">Battle report</param>
        /// <returns>Gang level up report</returns>
        GangLevelUpReport ProcessExperience(BattleReport battleReport);

        /// <summary>
        /// Gets the underdog bonus for beating a gang with a higher rating
        /// </summary>
        /// <param name="gangRating">Gang rating</param>
        /// <param name="opponentGangRating">Opponent gang rating</param>
        /// <param name="hasWon">True if gang won battle</param>
        /// <returns>Underdog bonus as integer</returns>
        int GetUnderdogBonus(int gangRating, int opponentGangRating, bool hasWon);

        /// <summary>
        /// Get bonus for wounding hits.
        /// </summary>
        /// <param name="woundingHits">Number of wounding hits</param>
        /// <returns>Wounding hits bonus</returns>
        int GetWoundingHitBonus(int woundingHits);

        /// <summary>
        /// Gets the leader's bonus after a battle.
        /// </summary>
        /// <param name="ganger">Leader</param>
        /// <param name="gameType">Game type</param>
        /// <param name="hasWon">True if game won</param>
        /// <param name="isAttacker">True if gang was attacking</param>
        /// <returns>Leader bonus</returns>
        int GetLeaderBonus(Ganger ganger, GameType gameType, bool hasWon, bool isAttacker);

        /// <summary>
        /// Get the number of advance rolls
        /// </summary>
        /// <param name="ganger">Ganger levelling up</param>
        /// <param name="experience">Experience gained</param>
        /// <returns>Number of rolls on the advance chart</returns>
        int GetNumberOfAdvanceRolls(Ganger ganger, int experience);

        /// <summary>
        /// Get bonus for achieving objectives.
        /// </summary>
        /// <param name="objectives">Objectives</param>
        /// <param name="gameType">Game type</param>
        /// <returns>Objective bonus</returns>
        int GetObjectivesBonus(int objectives, GameType gameType);

        /// <summary>
        /// Get bonus for winning battle.
        /// </summary>
        /// <param name="hasWon">True if game won</param>
        /// <param name="gameType">Game type</param>
        /// <returns>Winning bonus</returns>
        int GetWinningBonus(bool hasWon, GameType gameType);

        /// <summary>
        /// Get the survival bonus
        /// </summary>
        /// <returns>Survival bonus</returns>
        int GetSurvivalBonus();

        /// <summary>
        /// Get a gang skill for advance rolls.
        /// </summary>
        /// <param name="type">Ganger type</param>
        /// <param name="house">Gang house</param>
        /// <returns>List of skill types to choose from.</returns>
        IEnumerable<SkillType> GetGangSkill(GangerType type, GangHouse house);

        /// <summary>
        /// Do an advance roll
        /// </summary>
        /// <param name="ganger">Ganger advancing</param>
        /// <param name="house">Ganger's house</param>
        /// <returns>Ganger level up report</returns>
        GangerLevelUpReport DoAdvanceRoll(Ganger ganger, GangHouse house);
    }
}
