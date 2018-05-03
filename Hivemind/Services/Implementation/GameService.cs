// <copyright file="GameService.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using Hivemind.Contracts;
using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Managers;

namespace Hivemind.Services.Implementation
{
    /// <summary>
    /// Game service implementation
    /// </summary>
    public class GameService : IGameService
    {
        private IInjuryService _injuryService;
        private IIncomeService _incomeService;
        private IExperienceService _experienceService;
        private IGangerManager _gangerManager;

        /// <summary>
        /// Initializes a new instance of the <see cref="GameService"/> class.
        /// </summary>
        /// <param name="injuryService">Injury service</param>
        /// <param name="incomeService">Income service</param>
        /// <param name="experienceService">Experience service</param>
        /// <param name="gangerManager">Ganger manager</param>
        public GameService(
            IInjuryService injuryService,
            IIncomeService incomeService,
            IExperienceService experienceService,
            IGangerManager gangerManager)
        {
            _injuryService = injuryService ?? throw new ArgumentNullException(nameof(injuryService));
            _incomeService = incomeService ?? throw new ArgumentNullException(nameof(incomeService));
            _experienceService = experienceService ?? throw new ArgumentNullException(nameof(experienceService));
            _gangerManager = gangerManager ?? throw new ArgumentNullException(nameof(gangerManager));
        }

        /// <summary>
        /// Process pre game events, such as Old Battle Wound, Spore Sickness, etc. Anything that needs to happen 
        /// before the battle should go here.
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>Pre game report</returns>
        public PreGameReport ProcessPreGame(string gangId)
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Process post game. Determine injuries, experience, income.
        /// </summary>
        /// <param name="battleReport">Battle report</param>
        /// <returns>Post game report</returns>
        public PostGameReport ProcessPostGame(BattleReport battleReport)
        {
            var injuryReport = _injuryService.ProcessInjuries(battleReport);
            var experienceReport = _experienceService.ProcessExperience(battleReport);

            // Unfortunately we need to get some info from the first two reports to process income.
            var deaths = injuryReport.Injuries
                .Select(gangerReport =>
                    gangerReport.Injuries.Select(
                        injury => injury.InjuryId == InjuryEnum.Dead
                    ).Count()
                ).Count();

            var incomeReport = _incomeService.ProcessIncome(battleReport, deaths);

            return new PostGameReport()
            {
                Injuries = injuryReport,
                Experience = experienceReport,
                Income = incomeReport,
            };
        }

        /// <summary>
        /// Skill up gangers. Requires user interaction, so happens after post game is processed.
        /// </summary>
        /// <param name="skillUpRequest">Skill up request</param>
        /// <returns>List of ganger skills.</returns>
        public IEnumerable<GangerSkill> SkillUpGangers(GangSkillUpRequest skillUpRequest)
        {
            var response = new List<GangerSkill>();

            foreach (var request in skillUpRequest.GangerSkillUpRequests)
            {
                var ganger = _gangerManager.GetGanger(request.GangerId);
                response.Add(_gangerManager.LearnSkill(ganger, request.AdvancementId, request.SkillCategory));
            }

            return response;
        }
    }
}
