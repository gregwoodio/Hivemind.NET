// <copyright file="IIncomeService..cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using Hivemind.Contracts;

namespace Hivemind.Services
{
    /// <summary>
    /// Income service
    /// </summary>
    public interface IIncomeService
    {
        /// <summary>
        /// Process income
        /// </summary>
        /// <param name="battleReport">Battle reports</param>
        /// <param name="deaths">Number of gangers who've died in the last battle.</param>
        /// <returns>Income report</returns>
        IncomeReport ProcessIncome(BattleReport battleReport, int deaths);

        /// <summary>
        /// Get gang upkeep.
        /// </summary>
        /// <param name="gangSize">Gang size</param>
        /// <param name="income">Income</param>
        /// <returns>Upkeep</returns>
        int GetGangUpkeep(int gangSize, int income);

        /// <summary>
        /// Get giant killer bonus
        /// </summary>
        /// <param name="gangRating">Gang rating</param>
        /// <param name="opponentGangRating">Opponent gang rating</param>
        /// <returns>Giant killer bonus</returns>
        int GetGiantKillerBonus(int gangRating, int opponentGangRating);
    }
}
