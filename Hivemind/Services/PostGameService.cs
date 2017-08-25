using Hivemind.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hivemind.Enums;

namespace Hivemind.Services
{
    public class PostGameService : IPostGameService
    {
        private IInjuryService _injuryService;
        private IIncomeService _incomeService;
        private IExperienceService _experienceService;

        public PostGameService(IInjuryService injuryService, IIncomeService incomeService, IExperienceService experienceService)
        {
            _injuryService = injuryService ?? throw new ArgumentNullException(nameof(injuryService));
            _incomeService = incomeService ?? throw new ArgumentNullException(nameof(incomeService));
            _experienceService = experienceService ?? throw new ArgumentNullException(nameof(experienceService));
        }

        public PostGameReport ProcessPostGame(BattleReport battleReport)
        {
            var injuryReport = _injuryService.ProcessInjuries(battleReport);
            var experienceReport = _experienceService.ProcessExperience(battleReport);

            // Unfortunately we need to get some info from the first two reports to process income.
            var deaths = injuryReport.Injuries
                .Select(gangerReport =>
                    gangerReport.Injuries.Select(
                        injury => injury.InjuryId == InjuryEnum.DEAD
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
    }
}
