using Hivemind.Contracts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Services
{
    public class PostGameService : IPostGameService
    {
        public PostGameReport ProcessPostGame(BattleReport battleReport)
        {
            //TODO: use injection
            return new PostGameReport()
            {
                Injuries = new InjuryService().ProcessInjuries(battleReport),
                Experience = new ExperienceService().ProcessExperience(battleReport),
                Income = new IncomeService().ProcessIncome(battleReport)
            };
        }
    }
}
