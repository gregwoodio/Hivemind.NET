using Hivemind.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hivemind.Enums;
using Hivemind.Managers;
using Hivemind.Entities;

namespace Hivemind.Services.Implementation
{
    public class GameService : IGameService
    {
        private IInjuryService _injuryService;
        private IIncomeService _incomeService;
        private IExperienceService _experienceService;
        private IGangerManager _gangerManager;

        public GameService(IInjuryService injuryService, 
            IIncomeService incomeService, 
            IExperienceService experienceService,
            IGangerManager gangerManager)
        {
            _injuryService = injuryService ?? throw new ArgumentNullException(nameof(injuryService));
            _incomeService = incomeService ?? throw new ArgumentNullException(nameof(incomeService));
            _experienceService = experienceService ?? throw new ArgumentNullException(nameof(experienceService));
            _gangerManager = gangerManager ?? throw new ArgumentNullException(nameof(gangerManager));
        }

        public PreGameReport ProcessPreGame(string gangId)
        {
            throw new NotImplementedException();
        }

        public PostGameReport ProcessPostGame(BattleReport battleReport)
        {
            var injuryReport = _injuryService.ProcessInjuries(battleReport);
            var experienceReport = _experienceService.ProcessExperience(battleReport);

            // Unfortunately we need to get some info from the first two reports to process income.
            var deaths = injuryReport.Injuries
                .Select(gangerReport =>
                    gangerReport.Injuries.Select(
                        injury => injury.InjuryEnum == InjuryEnum.Dead
                    ).Count()
                ).Count();

            var incomeReport = _incomeService.ProcessIncome(battleReport, deaths);

            return new PostGameReport()
            {
                Injuries = injuryReport,
                Experience = experienceReport,
                Income = incomeReport
            };
        }

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
