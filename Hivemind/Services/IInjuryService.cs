// <copyright file="IInjuryService.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using Hivemind.Contracts;

namespace Hivemind.Services
{
    /// <summary>
    /// Injury service
    /// </summary>
    public interface IInjuryService
    {
        /// <summary>
        /// Process injuries
        /// </summary>
        /// <param name="battleReport">Battle report</param>
        /// <returns>Injury report</returns>
        InjuryReport ProcessInjuries(BattleReport battleReport);
    }
}
