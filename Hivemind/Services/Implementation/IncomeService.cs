using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hivemind.Contracts;
using Hivemind.Managers;
using Hivemind.Entities;
using Hivemind.Utilities;

namespace Hivemind.Services.Implementation
{
    public class IncomeService : IIncomeService
    {
        private IGangManager _gangManager;
        private ITerritoryManager _territoryManager;

        private const int MaximumTerritoriesWorked = 10;

        public IncomeService(IGangManager gangManager, ITerritoryManager territoryManager)
        {
            _gangManager = gangManager ?? throw new ArgumentNullException(nameof(gangManager));
            _territoryManager = territoryManager ?? throw new ArgumentNullException(nameof(territoryManager));
        }

        public IncomeReport ProcessIncome(BattleReport battleReport, int deaths)
        {
            var gang = _gangManager.GetGang(battleReport.GangId);
            var territories = _territoryManager.GetTerritoriesByGangId(battleReport.GangId).ToList();

            territories.Sort();
            var gangers = GetGangers(battleReport.GangId).ToList();
            var gross = new List<TerritoryIncomeReport>();

            for (int i = 0; i < gangers.Count() && i < territories.Count && i < MaximumTerritoriesWorked; i++)
            {
                var status = new TerritoryWorkStatus()
                {
                    TerritoryName = territories[i].Name,
                    Deaths = deaths,
                    Ganger = gangers[i],
                    GangId = battleReport.GangId,
                    Objectives = battleReport.GangBattleStats.Select(stats => stats.Objectives).Sum(),
                    PreviousBattleType = battleReport.GameType,
                    Roll = DiceRoller.ParseDiceString(territories[i].Income)
                };
                
                gross.Add(territories[i].WorkTerritory(status));
            }

            int territoryGross = gross.Select(territoryReport => territoryReport.Income).Sum();
            int giantKillerBonus = GetGiantKillerBonus(gang.GangRating, battleReport.OpponentGangRating);
            int incomeAfterUpkeep = GetGangUpkeep(GetNumberOfGangMembers(battleReport.GangId), territoryGross + giantKillerBonus);

            var report = new IncomeReport()
            {
                Gross = gross,
                GiantKillerBonus = giantKillerBonus,
                Upkeep = territoryGross + giantKillerBonus - incomeAfterUpkeep,
                Income = incomeAfterUpkeep
            };

            gang.Credits += report.Income;
            _gangManager.UpdateGang(gang);

            return report;
        }

        public int GetGangUpkeep(int gangSize, int income)
        {
            int col = 0;
            int row = 0;
            int[,] deductionTable = new int[,] {
                { 15,  10,   5,   0,   0,   0,  0},
                { 25,  20,  15,   5,   0,   0,  0},
                { 35,  30,  25,  15,   5,   0,  0},
                { 50,  45,  40,  30,  20,   5,  0},
                { 65,  60,  55,  45,  35,  15,  0},
                { 85,  80,  75,  65,  55,  35, 15},
                {105, 100,  95,  85,  75,  55, 35},
                {120, 115, 110, 100,  90,  65, 45},
                {135, 130, 125, 115, 105,  80, 55},
                {145, 140, 135, 125, 115,  90, 65},
                {155, 150, 145, 135, 125, 100, 70}
            };

            //get column
            if (gangSize < 4)
            {
                col = 0;
            }
            else if (gangSize < 7) 
            {
                col = 1;
            }
            else if (gangSize < 10)
            {
                col = 2;
            }
            else if (gangSize < 13)
            {
                col = 3;
            }
            else if (gangSize < 16)
            {
                col = 4;
            }
            else if (gangSize < 19)
            {
                col = 5;
            }
            else
            {
                col = 6;
            }

            //get row
            if (income <= 29)
            {
                row = 0;
            }
            else if (income <= 49)
            {
                row = 1;
            }
            else if (income <= 79)
            {
                row = 2;
            }
            else if (income <= 119)
            {
                row = 3;
            }
            else if (income <= 169)
            {
                row = 4;
            }
            else if (income <= 229)
            {
                row = 5;
            }
            else if (income <= 299)
            {
                row = 6;
            }
            else if (income <= 379)
            {
                row = 7;
            }
            else if (income <= 459)
            {
                row = 8;
            }
            else if (income <= 559)
            {
                row = 9;
            }
            else if (income <= 669)
            {
                row = 10;
            }

            return deductionTable[row, col];
        }

        public int GetGiantKillerBonus(int gangRating, int opponentGangRating)
        {
            if (gangRating > opponentGangRating)
            {
                return 0;
            }

            int difference = opponentGangRating - gangRating;
            if (difference < 50)
            {
                return 5;
            } 
            else if (difference < 100)
            {
                return 10;
            }
            else if (difference < 150)
            {
                return 15;
            }
            else if (difference < 200)
            {
                return 20;
            }
            else if (difference < 250)
            {
                return 25;
            }
            else if (difference < 500)
            {
                return 50;
            }
            else if (difference < 750)
            {
                return 100;
            }
            else if (difference < 1000)
            {
                return 150;
            }
            else if (difference < 1500)
            {
                return 200;
            }
            else
            {
                return 250;
            }
        }

        private int GetNumberOfGangMembers(string gangId)
        {
            return _gangManager.GetGang(gangId).Gangers.Count();
        }

        /// <summary>
        /// Only Gangers can work territories. Return a list of Gangers in the gang.
        /// </summary>
        /// <param name="gangId"></param>
        /// <returns></returns>
        private IEnumerable<Ganger> GetGangers(string gangId)
        {
            return _gangManager.GetGang(gangId).Gangers.Where(ganger => ganger.GangerType == Enums.GangerType.Ganger);
        }
    }
}
