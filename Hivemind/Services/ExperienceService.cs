using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hivemind.Contracts;
using Hivemind.Factories;
using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Utilities;

namespace Hivemind.Services
{
    public class ExperienceService : IExperienceService
    {
        public GangLevelUpReport ProcessExperience(BattleReport battleReport)
        {
            var gangFactory = new GangFactory();
            var gangerFactory = new GangerFactory();
            var gang = gangFactory.GetGang(battleReport.GangId);
            var underdogBonus = GetUnderdogBonus(gang.GangRating, battleReport.OpponentGangRating, battleReport.HasWon);

            var advancements = new List<GangerLevelUpReport>();
            foreach (var gangerStats in battleReport.GangBattleStats)
            {
                var ganger = gangerFactory.GetGanger(gangerStats.GangerId);
                var experience = 0;
                experience += GetLeaderBonus(ganger, battleReport.GameType, battleReport.HasWon, battleReport.IsAttacker);
                experience += GetWoundingHitBonus(gangerStats.Kills);
                experience += GetObjectivesBonus(gangerStats.Objectives, battleReport.GameType);
                experience += GetWinningBonus(battleReport.HasWon, battleReport.GameType);
                experience += GetSurvivalBonus();

                advancements.Add(LevelUp(ganger, experience, gang.House));
            }

            return new GangLevelUpReport()
            {
                GangerAdvancements = advancements
            };
        }

        private GangerLevelUpReport LevelUp(Ganger ganger, int experience, GangHouse house)
        {
            var skillChoices = new List<SkillType>();

            return new GangerLevelUpReport()
            {
                Description = "",
                GangerId = ganger.GangerId,
                NewSkillFromCategory = skillChoices
            };
        }

        private int GetUnderdogBonus(int gangRating, int opponentGangRating, bool hasWon)
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

        private int GetLeaderBonus(Ganger ganger, GameType gameType, bool hasWon, bool isAttacker)
        {
            if (ganger.Type != GangerType.LEADER)
            {
                return 0;
            }
            
            switch (gameType)
            {
                case GameType.GANG_FIGHT:
                case GameType.SCAVENGERS:
                case GameType.AMBUSH:
                    return hasWon ? 10 : 0;
                case GameType.RESCUE_MISSION:
                    return hasWon && !isAttacker ? 10 : 0;
                default:
                    return 0;
            }
        }

        private int GetWoundingHitBonus(int woundingHits)
        {
            return woundingHits * 5;
        }

        private int GetObjectivesBonus(int objectives, GameType gameType)
        {
            switch (gameType)
            {
                case GameType.SCAVENGERS:
                    return objectives;
                case GameType.THE_RAID:
                case GameType.RESCUE_MISSION:
                    return objectives * 5;
                default:
                    return 0;
            }
        }

        private int GetWinningBonus(bool hasWon, GameType gameType)
        {
            if (gameType == GameType.HIT_AND_RUN && hasWon)
            {
                return 10;
            }
            return 0;
        }

        private int GetSurvivalBonus()
        {
            return DiceRoller.RollDie();
        }
    }
}
