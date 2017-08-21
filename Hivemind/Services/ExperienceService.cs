using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hivemind.Contracts;

namespace Hivemind.Services
{
    public class ExperienceService : IExperienceService
    {
        public GangLevelUpReport ProcessExperience(BattleReport battleReport)
        {
            throw new NotImplementedException();
            //var gang = 
            //var underdogBonus = DetermineUnderdogBonus(battleReport.Gang)
        }

        private int DetermineUnderdogBonus(int gangRating, int opponentGangRating, bool hasWon)
        {
            int diff = Math.Abs(opponentGangRating - gangRating);
            int bonus = 0;
            if (diff <= 49)
            {
                bonus = hasWon ? 1 : 0;
            }
            else if (diff >= 50 && diff <= 99)
            {
                bonus = hasWon ? 2 : 1;
            }
            else if (diff >= 100 && diff <= 149)
            {
                bonus = hasWon ? 3 : 2;
            }
            else if (diff >= 150 && diff <= 199)
            {
                bonus = hasWon ? 4 : 3;
            }
            else if (diff >= 200 && diff <= 249)
            {
                bonus = hasWon ? 5 : 4;
            }
            else if (diff >= 250 && diff <= 499)
            {
                bonus = hasWon ? 6 : 5;
            }
            else if (diff >= 500 && diff <= 749)
            {
                bonus = hasWon ? 7 : 6;
            }
            else if (diff >= 750 && diff <= 999)
            {
                bonus = hasWon ? 8 : 7;
            }
            else if (diff >= 1000 && diff <= 1499)
            {
                bonus = hasWon ? 9 : 8;
            }
            else if (diff >= 1500)
            {
                bonus = hasWon ? 10 : 9;
            }
            return bonus;
        }
    }
}
