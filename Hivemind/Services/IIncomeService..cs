using Hivemind.Contracts;
using Hivemind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Services
{
    public interface IIncomeService
    {
        IncomeReport ProcessIncome(BattleReport battleReport, int deaths);
        int GetGangUpkeep(int gangSize, int income);
        int GetGiantKillerBonus(int gangRating, int opponentGangRating);
    }
}
