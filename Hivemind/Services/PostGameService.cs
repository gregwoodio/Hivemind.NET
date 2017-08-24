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

        public PostGameService(IInjuryService injuryService, IIncomeService incomeService)
        {
            _injuryService = injuryService ?? throw new ArgumentNullException(nameof(injuryService));
            _incomeService = incomeService ?? throw new ArgumentNullException(nameof(incomeService));
        }

        public PostGameReport ProcessPostGame(BattleReport battleReport)
        {
            //TODO: use injection
            var injuryReport = new InjuryService().ProcessInjuries(battleReport);
            var experienceReport = new ExperienceService().ProcessExperience(battleReport);

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
