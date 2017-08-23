using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hivemind.Contracts;
using Hivemind.Factories;
using Hivemind.Entities;
using Hivemind.Utilities;

namespace Hivemind.Services
{
    public class IncomeService : IIncomeService
    {
        // TODO: use injection
        GangFactory gangFactory = new GangFactory();
        TerritoryFactory territoryFactory = new TerritoryFactory();

        public IncomeReport ProcessIncome(BattleReport battleReport)
        {
            var gang = gangFactory.GetGang(battleReport.GangId);
            var territories = territoryFactory.GetTerritoriesByGangId(battleReport.GangId);

            int gross = 0;
            foreach (var territory in territories)
            {
                gross += ParseDiceNomenclature(territory.Income);
            }
            int giantKillerBonus = GetGiantKillerBonus(gang, battleReport.OpponentGangRating);
            int upkeep = GetGangUpkeep(gang.GangId, gross + giantKillerBonus);

            var report = new IncomeReport()
            {
                Gross = gross,
                GiantKillerBonus = giantKillerBonus,
                Deductions = upkeep,
                Income = gross + giantKillerBonus - upkeep
            };

            gang.Credits += report.Income;
            gangFactory.UpdateGang(gang);

            return report;
        }

        private int GetNumberOfGangMembers(int gangId)
        {
            return gangFactory.GetGang(gangId).Gangers.Count();
        }

        private int GetNumberOfGangers(int gangId)
        {
            return gangFactory.GetGang(gangId).Gangers.Where(ganger => ganger.Type == Enums.GangerType.GANGER).Count();
        }

        private int GetGangUpkeep(int gangId, int income)
        {
            int gangSize = GetNumberOfGangMembers(gangId);
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

        private int GetGiantKillerBonus(Gang gang, int opponentGangRating)
        {
            if (gang.GangRating < opponentGangRating)
            {
                return 0;
            }

            int difference = gang.GangRating - opponentGangRating;
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

        /// <summary>
        /// Parse dice strings like 2D6, D6*10 into rolled values.
        /// </summary>
        /// <param name="dice"></param>
        /// <returns></returns>
        private int ParseDiceNomenclature(string dice)
        {
            int output;
            //income will be in the form D6*10, or 10, or 2D6
            String[] calc = dice.Split('*');
            if (calc.Length == 1)
            {
                if (calc[0][0] == '2')
                { //must be 2D6
                    return DiceRoller.RollDice(6, 2);
                }
                else
                {
                    //not a die, must be a number
                    if (Int32.TryParse(calc[0], out output))
                    {
                        return output;
                    }
                    else
                    {
                        //laziness
                        return 10;
                    }
                }
            }
            else if (calc.Length == 2)
            {
                if (calc[0][0] == '2')
                { //2D6 * __
                    if (Int32.TryParse(calc[1], out output))
                    {
                        return output * DiceRoller.RollDice(6, 2);
                    }
                    else
                    {
                        return 10 * DiceRoller.RollDice(6, 2);
                    }
                }
                else if (calc[0][0] == 'D')
                { //D6 * __
                    if (Int32.TryParse(calc[1], out output))
                    {
                        return output * DiceRoller.RollDie();
                    }
                    else
                    {
                        return 10 * DiceRoller.RollDie();
                    }
                }
            }
            return 0;
        }
    }
}
