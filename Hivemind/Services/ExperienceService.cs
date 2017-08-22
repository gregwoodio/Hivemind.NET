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
using Hivemind.Exceptions;

namespace Hivemind.Services
{
    public class ExperienceService : IExperienceService
    {
        GangerFactory gangerFactory = new GangerFactory();

        public GangLevelUpReport ProcessExperience(BattleReport battleReport)
        {
            var gangFactory = new GangFactory();
            
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

                var advanceRolls = GetNumberOfAdvanceRolls(ganger, experience);
                while (advanceRolls > 0)
                {
                    advancements.Add(DoAdvanceRoll(ganger, gang.House));
                    advanceRolls--;
                }

                ganger.Experience += experience;
                gangerFactory.UpdateGanger(ganger);
            }

            return new GangLevelUpReport()
            {
                GangerAdvancements = advancements
            };
        }

        private int GetNumberOfAdvanceRolls(Ganger ganger, int experience)
        {
            int advanceRolls = 0;
            int nextLevelExperience = 0;

            while (experience > 0)
            {
                if (ganger.Experience <= 5)
                {
                    nextLevelExperience = 6;
                }
                else if (ganger.Experience <= 10)
                {
                    nextLevelExperience = 11;
                }
                else if (ganger.Experience <= 15)
                {
                    nextLevelExperience = 16;
                }
                else if (ganger.Experience <= 20)
                {
                    nextLevelExperience = 21;
                }
                else if (ganger.Experience <= 30)
                {
                    nextLevelExperience = 31;
                }
                else if (ganger.Experience <= 40)
                {
                    nextLevelExperience = 41;
                }
                else if (ganger.Experience <= 50)
                {
                    nextLevelExperience = 51;
                }
                else if (ganger.Experience <= 60)
                {
                    nextLevelExperience = 61;
                }
                else if (ganger.Experience <= 80)
                {
                    nextLevelExperience = 81;
                }
                else if (ganger.Experience <= 100)
                {
                    nextLevelExperience = 101;
                }
                else if (ganger.Experience <= 120)
                {
                    nextLevelExperience = 121;
                }
                else if (ganger.Experience <= 140)
                {
                    nextLevelExperience = 141;
                }
                else if (ganger.Experience <= 160)
                {
                    nextLevelExperience = 161;
                }
                else if (ganger.Experience <= 180)
                {
                    nextLevelExperience = 181;
                }
                else if (ganger.Experience <= 200)
                {
                    nextLevelExperience = 201;
                }
                else if (ganger.Experience <= 240)
                {
                    nextLevelExperience = 241;
                }
                else if (ganger.Experience <= 280)
                {
                    nextLevelExperience = 281;
                }
                else if (ganger.Experience <= 320)
                {
                    nextLevelExperience = 321;
                }
                else if (ganger.Experience <= 360)
                {
                    nextLevelExperience = 361;
                }
                else if (ganger.Experience <= 400)
                {
                    nextLevelExperience = 401;
                }
                else
                {
                    nextLevelExperience = Int32.MaxValue;
                }

                if ((ganger.Experience + experience) > nextLevelExperience)
                {
                    ganger.Experience = nextLevelExperience;
                    experience -= nextLevelExperience;
                    advanceRolls++;
                }
                else
                {
                    ganger.Experience += experience;
                }
            }

            return advanceRolls;
        }

        private GangerLevelUpReport DoAdvanceRoll(Ganger ganger, GangHouse house)
        {
            GangerStatistics stat = 0;
            int roll = DiceRoller.RollDice(6, 2);
            int statToIncrease = DiceRoller.RollDie();

            switch (roll)
            {
                case 2:
                case 12:
                    return new GangerLevelUpReport()
                    {
                        Description = "Pick any skill",
                        GangerName = ganger.Name,
                        NewSkillFromCategory = new[] { SkillType.AGILITY, SkillType.COMBAT, SkillType.FEROCITY, SkillType.MUSCLE, SkillType.SHOOTING, SkillType.STEALTH, SkillType.TECHNO }
                    };
                case 3:
                case 4:
                case 10:
                case 11:
                    return new GangerLevelUpReport()
                    {
                        Description = "New gang skill",
                        GangerName = ganger.Name,
                        NewSkillFromCategory = GetGangSkill(ganger.Type, house)
                    };
                case 5:
                    stat = (statToIncrease <= 3) ? GangerStatistics.STRENGTH : GangerStatistics.ATTACK;
                    gangerFactory.IncreaseStat(ganger, stat, null);
                    return new GangerLevelUpReport()
                    {
                        Description = Enum.GetName(typeof(GangerStatistics), stat) + " increased",
                        GangerName = ganger.Name,
                        NewSkillFromCategory = null
                    };
                case 6:
                case 8:
                    stat = (statToIncrease <= 3) ? GangerStatistics.WEAPON_SKILL : GangerStatistics.BALLISTIC_SKILL;
                    gangerFactory.IncreaseStat(ganger, stat, null);
                    return new GangerLevelUpReport()
                    {
                        Description = Enum.GetName(typeof(GangerStatistics), stat) + " increased",
                        GangerName = ganger.Name,
                        NewSkillFromCategory = null
                    };
                case 7:
                    stat = (statToIncrease <= 3) ? GangerStatistics.INITIATIVE : GangerStatistics.LEADERSHIP;
                    gangerFactory.IncreaseStat(ganger, stat, null);
                    return new GangerLevelUpReport()
                    {
                        Description = Enum.GetName(typeof(GangerStatistics), stat) + " increased",
                        GangerName = ganger.Name,
                        NewSkillFromCategory = null
                    };
                case 9:
                    stat = (statToIncrease <= 3) ? GangerStatistics.WOUNDS : GangerStatistics.ATTACK;
                    gangerFactory.IncreaseStat(ganger, stat, null);
                    return new GangerLevelUpReport()
                    {
                        Description = Enum.GetName(typeof(GangerStatistics), stat) + " increased",
                        GangerName = ganger.Name,
                        NewSkillFromCategory = null
                    };
            }
            return null;
        }

        private IEnumerable<SkillType> GetGangSkill(GangerType type, GangHouse house)
        {
            SkillType[] skillList = new SkillType[0];
            switch (house)
            {
                case GangHouse.CAWDOR:
                    switch (type)
                    {
                        case GangerType.JUVE:
                            skillList = new[] { SkillType.COMBAT, SkillType.FEROCITY };
                            break;
                        case GangerType.GANGER:
                            skillList = new[] { SkillType.COMBAT, SkillType.FEROCITY, SkillType.AGILITY };
                            break;
                        case GangerType.HEAVY:
                            skillList = new[] { SkillType.FEROCITY, SkillType.MUSCLE, SkillType.SHOOTING, SkillType.TECHNO };
                            break;
                        case GangerType.LEADER:
                            skillList = new[] { SkillType.AGILITY, SkillType.COMBAT, SkillType.FEROCITY, SkillType.MUSCLE, SkillType.SHOOTING, SkillType.TECHNO };
                            break;
                    }
                    break;
                case GangHouse.ESCHER:
                    switch (type)
                    {
                        case GangerType.JUVE:
                            skillList = new[] { SkillType.AGILITY, SkillType.COMBAT };
                            break;
                        case GangerType.GANGER:
                            skillList = new[] { SkillType.AGILITY, SkillType.COMBAT, SkillType.STEALTH };
                            break;
                        case GangerType.HEAVY:
                            skillList = new[] { SkillType.AGILITY, SkillType.MUSCLE, SkillType.SHOOTING, SkillType.TECHNO };
                            break;
                        case GangerType.LEADER:
                            skillList = new[] { SkillType.AGILITY, SkillType.COMBAT, SkillType.FEROCITY, SkillType.SHOOTING, SkillType.STEALTH, SkillType.TECHNO };
                            break;
                    }
                    break;
                case GangHouse.DELAQUE:
                    switch (type)
                    {
                        case GangerType.JUVE:
                            skillList = new[] { SkillType.SHOOTING, SkillType.STEALTH };
                            break;
                        case GangerType.GANGER:
                            skillList = new[] { SkillType.AGILITY, SkillType.SHOOTING, SkillType.STEALTH };
                            break;
                        case GangerType.HEAVY:
                            skillList = new[] { SkillType.MUSCLE, SkillType.STEALTH, SkillType.SHOOTING, SkillType.TECHNO };
                            break;
                        case GangerType.LEADER:
                            skillList = new[] { SkillType.AGILITY, SkillType.COMBAT, SkillType.FEROCITY, SkillType.SHOOTING, SkillType.STEALTH, SkillType.TECHNO };
                            break;
                    }
                    break;
                case GangHouse.GOLIATH:
                    switch (type)
                    {
                        case GangerType.JUVE:
                            skillList = new[] { SkillType.FEROCITY, SkillType.MUSCLE };
                            break;
                        case GangerType.GANGER:
                            skillList = new[] { SkillType.FEROCITY, SkillType.MUSCLE, SkillType.COMBAT };
                            break;
                        case GangerType.HEAVY:
                            skillList = new[] { SkillType.MUSCLE, SkillType.COMBAT, SkillType.SHOOTING, SkillType.TECHNO };
                            break;
                        case GangerType.LEADER:
                            skillList = new[] { SkillType.COMBAT, SkillType.FEROCITY, SkillType.MUSCLE, SkillType.SHOOTING, SkillType.STEALTH, SkillType.TECHNO };
                            break;
                    }
                    break;
                case GangHouse.ORLOCK:
                    switch (type)
                    {
                        case GangerType.JUVE:
                            skillList = new[] { SkillType.FEROCITY, SkillType.SHOOTING };
                            break;
                        case GangerType.GANGER:
                            skillList = new[] { SkillType.COMBAT, SkillType.FEROCITY, SkillType.SHOOTING };
                            break;
                        case GangerType.HEAVY:
                            skillList = new[] { SkillType.COMBAT, SkillType.MUSCLE, SkillType.SHOOTING, SkillType.TECHNO };
                            break;
                        case GangerType.LEADER:
                            skillList = new[] { SkillType.AGILITY, SkillType.COMBAT, SkillType.FEROCITY, SkillType.SHOOTING, SkillType.STEALTH, SkillType.TECHNO };
                            break;
                    }
                    break;
                case GangHouse.VAN_SAAR:
                    switch (type)
                    {
                        case GangerType.JUVE:
                            skillList = new[] { SkillType.SHOOTING, SkillType.TECHNO };
                            break;
                        case GangerType.GANGER:
                            skillList = new[] { SkillType.COMBAT, SkillType.SHOOTING, SkillType.TECHNO };
                            break;
                        case GangerType.HEAVY:
                            skillList = new[] { SkillType.COMBAT, SkillType.MUSCLE, SkillType.SHOOTING, SkillType.TECHNO };
                            break;
                        case GangerType.LEADER:
                            skillList = new[] { SkillType.AGILITY, SkillType.COMBAT, SkillType.FEROCITY, SkillType.SHOOTING, SkillType.STEALTH, SkillType.TECHNO };
                            break;
                    }
                    break;
                default:
                    HivemindException.NoSuchGangHouse();
                    break;
            }
            return skillList;
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
