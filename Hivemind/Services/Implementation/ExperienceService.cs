using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Hivemind.Contracts;
using Hivemind.Managers;
using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Utilities;
using Hivemind.Exceptions;

namespace Hivemind.Services.Implementation
{
    public class ExperienceService : IExperienceService
    {
        private IGangerManager _gangerManager;
        private IGangManager _gangManager;
        private IDiceRoller _diceRoller;

        public ExperienceService(IGangerManager gangerManager, IGangManager gangManager, IDiceRoller diceRoller)
        {
            _gangerManager = gangerManager ?? throw new ArgumentNullException(nameof(gangerManager));
            _gangManager = gangManager ?? throw new ArgumentNullException(nameof(gangManager));
            _diceRoller = diceRoller ?? throw new ArgumentNullException(nameof(diceRoller));
        }

        public GangLevelUpReport ProcessExperience(BattleReport battleReport)
        {
            var gang = _gangManager.GetGang(battleReport.GangId);
            var underdogBonus = GetUnderdogBonus(gang.GangRating, battleReport.OpponentGangRating, battleReport.HasWon);

            var advancements = new List<GangerLevelUpReport>();
            foreach (var gangerStats in battleReport.GangBattleStats)
            {
                var ganger = _gangerManager.GetGanger(gangerStats.GangerId);
                var experience = 0;
                experience += GetLeaderBonus(ganger, battleReport.GameType, battleReport.HasWon, battleReport.IsAttacker);
                experience += GetWoundingHitBonus(gangerStats.Kills);
                experience += GetObjectivesBonus(gangerStats.Objectives, battleReport.GameType);
                experience += GetWinningBonus(battleReport.HasWon, battleReport.GameType);
                experience += GetSurvivalBonus();

                var advanceRolls = GetNumberOfAdvanceRolls(ganger, experience);
                while (advanceRolls > 0)
                {
                    advancements.Add(DoAdvanceRoll(ganger, gang.GangHouse));
                    advanceRolls--;
                }

                ganger.Experience += experience;
                _gangerManager.UpdateGanger(ganger);
            }

            return new GangLevelUpReport()
            {
                GangerAdvancements = advancements
            };
        }

        public int GetNumberOfAdvanceRolls(Ganger ganger, int experience)
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

                if ((ganger.Experience + experience) >= nextLevelExperience)
                {
                    experience = ganger.Experience + experience - nextLevelExperience;
                    ganger.Experience = nextLevelExperience;
                    advanceRolls++;
                }
                else
                {
                    ganger.Experience += experience;
                    experience = 0;
                }
            }

            return advanceRolls;
        }

        public int GetUnderdogBonus(int gangRating, int opponentGangRating, bool hasWon)
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

        public int GetLeaderBonus(Ganger ganger, GameType gameType, bool hasWon, bool isAttacker)
        {
            if (ganger.GangerType != GangerType.Leader)
            {
                return 0;
            }
            
            switch (gameType)
            {
                case GameType.GangFight:
                case GameType.Scavengers:
                case GameType.Ambush:
                    return hasWon ? 10 : 0;
                case GameType.RescueMission:
                    return hasWon && !isAttacker ? 10 : 0;
                default:
                    return 0;
            }
        }

        public int GetWoundingHitBonus(int woundingHits)
        {
            return woundingHits * 5;
        }

        public int GetObjectivesBonus(int objectives, GameType gameType)
        {
            switch (gameType)
            {
                case GameType.Scavengers:
                    return objectives;
                case GameType.TheRaid:
                case GameType.RescueMission:
                    return objectives * 5;
                default:
                    return 0;
            }
        }

        public int GetWinningBonus(bool hasWon, GameType gameType)
        {
            if (gameType == GameType.HitAndRun && hasWon)
            {
                return 10;
            }
            return 0;
        }

        public int GetSurvivalBonus()
        {
            return _diceRoller.RollDie();
        }

        public IEnumerable<SkillType> GetGangSkill(GangerType type, GangHouse house)
        {
            SkillType[] skillList = new SkillType[0];
            switch (house)
            {
                case GangHouse.Cawdor:
                    switch (type)
                    {
                        case GangerType.Juve:
                            skillList = new[] { SkillType.Combat, SkillType.Ferocity };
                            break;
                        case GangerType.Ganger:
                            skillList = new[] { SkillType.Combat, SkillType.Ferocity, SkillType.Agility };
                            break;
                        case GangerType.Heavy:
                            skillList = new[] { SkillType.Ferocity, SkillType.Muscle, SkillType.Shooting, SkillType.Techno };
                            break;
                        case GangerType.Leader:
                            skillList = new[] { SkillType.Agility, SkillType.Combat, SkillType.Ferocity, SkillType.Muscle, SkillType.Shooting, SkillType.Techno };
                            break;
                    }
                    break;
                case GangHouse.Escher:
                    switch (type)
                    {
                        case GangerType.Juve:
                            skillList = new[] { SkillType.Agility, SkillType.Combat };
                            break;
                        case GangerType.Ganger:
                            skillList = new[] { SkillType.Agility, SkillType.Combat, SkillType.Stealth };
                            break;
                        case GangerType.Heavy:
                            skillList = new[] { SkillType.Agility, SkillType.Muscle, SkillType.Shooting, SkillType.Techno };
                            break;
                        case GangerType.Leader:
                            skillList = new[] { SkillType.Agility, SkillType.Combat, SkillType.Ferocity, SkillType.Shooting, SkillType.Stealth, SkillType.Techno };
                            break;
                    }
                    break;
                case GangHouse.Delaque:
                    switch (type)
                    {
                        case GangerType.Juve:
                            skillList = new[] { SkillType.Shooting, SkillType.Stealth };
                            break;
                        case GangerType.Ganger:
                            skillList = new[] { SkillType.Agility, SkillType.Shooting, SkillType.Stealth };
                            break;
                        case GangerType.Heavy:
                            skillList = new[] { SkillType.Muscle, SkillType.Stealth, SkillType.Shooting, SkillType.Techno };
                            break;
                        case GangerType.Leader:
                            skillList = new[] { SkillType.Agility, SkillType.Combat, SkillType.Ferocity, SkillType.Shooting, SkillType.Stealth, SkillType.Techno };
                            break;
                    }
                    break;
                case GangHouse.Goliath:
                    switch (type)
                    {
                        case GangerType.Juve:
                            skillList = new[] { SkillType.Ferocity, SkillType.Muscle };
                            break;
                        case GangerType.Ganger:
                            skillList = new[] { SkillType.Ferocity, SkillType.Muscle, SkillType.Combat };
                            break;
                        case GangerType.Heavy:
                            skillList = new[] { SkillType.Muscle, SkillType.Combat, SkillType.Shooting, SkillType.Techno };
                            break;
                        case GangerType.Leader:
                            skillList = new[] { SkillType.Combat, SkillType.Ferocity, SkillType.Muscle, SkillType.Shooting, SkillType.Stealth, SkillType.Techno };
                            break;
                    }
                    break;
                case GangHouse.Orlock:
                    switch (type)
                    {
                        case GangerType.Juve:
                            skillList = new[] { SkillType.Ferocity, SkillType.Shooting };
                            break;
                        case GangerType.Ganger:
                            skillList = new[] { SkillType.Combat, SkillType.Ferocity, SkillType.Shooting };
                            break;
                        case GangerType.Heavy:
                            skillList = new[] { SkillType.Combat, SkillType.Muscle, SkillType.Shooting, SkillType.Techno };
                            break;
                        case GangerType.Leader:
                            skillList = new[] { SkillType.Agility, SkillType.Combat, SkillType.Ferocity, SkillType.Shooting, SkillType.Stealth, SkillType.Techno };
                            break;
                    }
                    break;
                case GangHouse.VanSaar:
                    switch (type)
                    {
                        case GangerType.Juve:
                            skillList = new[] { SkillType.Shooting, SkillType.Techno };
                            break;
                        case GangerType.Ganger:
                            skillList = new[] { SkillType.Combat, SkillType.Shooting, SkillType.Techno };
                            break;
                        case GangerType.Heavy:
                            skillList = new[] { SkillType.Combat, SkillType.Muscle, SkillType.Shooting, SkillType.Techno };
                            break;
                        case GangerType.Leader:
                            skillList = new[] { SkillType.Agility, SkillType.Combat, SkillType.Ferocity, SkillType.Shooting, SkillType.Stealth, SkillType.Techno };
                            break;
                    }
                    break;
                default:
                    HivemindException.NoSuchGangHouse();
                    break;
            }
            return skillList;
        }

        public GangerLevelUpReport DoAdvanceRoll(Ganger ganger, GangHouse house)
        {
            GangerStatistics stat = 0;
            int roll = _diceRoller.RollDice(6, 2);
            int statToIncrease = _diceRoller.RollDie();

            switch (roll)
            {
                case 2:
                case 12:
                    var advancementId = _gangerManager.RegisterGangerAdvancement(ganger.GangerId);
                    return new GangerLevelUpReport()
                    {
                        Description = "Pick any skill",
                        GangerName = ganger.Name,
                        GangerId = ganger.GangerId,
                        NewSkillFromCategory = new[] { SkillType.Agility, SkillType.Combat, SkillType.Ferocity, SkillType.Muscle, SkillType.Shooting, SkillType.Stealth, SkillType.Techno },
                        AdvancementId = advancementId
                    };
                case 3:
                case 4:
                case 10:
                case 11:
                    advancementId = _gangerManager.RegisterGangerAdvancement(ganger.GangerId);
                    return new GangerLevelUpReport()
                    {
                        Description = "New gang skill",
                        GangerName = ganger.Name,
                        GangerId = ganger.GangerId,
                        NewSkillFromCategory = GetGangSkill(ganger.GangerType, house),
                        AdvancementId = advancementId
                    };
                case 5:
                    stat = (statToIncrease <= 3) ? GangerStatistics.Strength : GangerStatistics.Attack;
                    _gangerManager.IncreaseStat(ganger, stat);
                    return new GangerLevelUpReport()
                    {
                        Description = Enum.GetName(typeof(GangerStatistics), stat) + " increased",
                        GangerName = ganger.Name,
                        GangerId = ganger.GangerId,
                        NewSkillFromCategory = null
                    };
                case 6:
                case 8:
                    stat = (statToIncrease <= 3) ? GangerStatistics.WeaponSkill : GangerStatistics.BallisticSkill;
                    _gangerManager.IncreaseStat(ganger, stat);
                    return new GangerLevelUpReport()
                    {
                        Description = Enum.GetName(typeof(GangerStatistics), stat) + " increased",
                        GangerName = ganger.Name,
                        GangerId = ganger.GangerId,
                        NewSkillFromCategory = null
                    };
                case 7:
                    stat = (statToIncrease <= 3) ? GangerStatistics.Initiative : GangerStatistics.Leadership;
                    _gangerManager.IncreaseStat(ganger, stat);
                    return new GangerLevelUpReport()
                    {
                        Description = Enum.GetName(typeof(GangerStatistics), stat) + " increased",
                        GangerName = ganger.Name,
                        GangerId = ganger.GangerId,
                        NewSkillFromCategory = null
                    };
                case 9:
                    stat = (statToIncrease <= 3) ? GangerStatistics.Wounds : GangerStatistics.Attack;
                    _gangerManager.IncreaseStat(ganger, stat);
                    return new GangerLevelUpReport()
                    {
                        Description = Enum.GetName(typeof(GangerStatistics), stat) + " increased",
                        GangerName = ganger.Name,
                        GangerId = ganger.GangerId,
                        NewSkillFromCategory = null
                    };
            }
            return null;
        }
    }
}
