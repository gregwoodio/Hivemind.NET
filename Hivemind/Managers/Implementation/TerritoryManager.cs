using Hivemind.Contracts;
using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Providers;
using Hivemind.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Managers.Implementation
{
    public class TerritoryManager : ITerritoryManager
    {
        private IInjuryManager _injuryFactory;
        private IGangerManager _gangerFactory;
        private IGangManager _gangFactory;
        private TerritoryProvider _territoryProvider;
        private Dictionary<TerritoryEnum, Func<TerritoryWorkStatus, TerritoryIncomeReport>> _territoryEffects;

        public TerritoryManager(IInjuryManager injuryFactory, IGangerManager gangerFactory, IGangManager gangFactory, TerritoryProvider territoryProvider)
        {
            _injuryFactory = injuryFactory ?? throw new ArgumentNullException(nameof(injuryFactory));
            _gangerFactory = gangerFactory ?? throw new ArgumentNullException(nameof(gangerFactory));
            _gangFactory = gangFactory ?? throw new ArgumentNullException(nameof(gangFactory));
            _territoryProvider = territoryProvider ?? throw new ArgumentNullException(nameof(territoryProvider));

            _territoryEffects = new Dictionary<TerritoryEnum, Func<TerritoryWorkStatus, TerritoryIncomeReport>>
            {
                { TerritoryEnum.CHEM_PIT, ChemPit },
                { TerritoryEnum.OLD_RUINS, NoTerritoryEffect },
                { TerritoryEnum.SLAG, NoTerritoryEffect },
                { TerritoryEnum.MINERAL_OUTCROP, NoTerritoryEffect },
                { TerritoryEnum.SETTLEMENT, Settlement },
                { TerritoryEnum.MINE_WORKINGS, MineWorkings },
                { TerritoryEnum.TUNNELS, NoTerritoryEffect },
                { TerritoryEnum.VENTS, NoTerritoryEffect },
                { TerritoryEnum.HOLESTEAD, NoTerritoryEffect },
                { TerritoryEnum.WATER_STILL, NoTerritoryEffect },
                { TerritoryEnum.DRINKING_HOLE, NoTerritoryEffect },
                { TerritoryEnum.GUILDER_CONTRACT, GuilderContract },
                { TerritoryEnum.FRIENDLY_DOC, FriendlyDoc },
                { TerritoryEnum.WORKSHOP, NoTerritoryEffect },
                { TerritoryEnum.GAMBLING_DEN, GamblingDen },
                { TerritoryEnum.SPORE_CAVE, SporeCave },
                { TerritoryEnum.ARCHEOTECH, Archeotech },
                { TerritoryEnum.GREEN_HIVERS, NoTerritoryEffect }
            };
        }

        public Territory GetTerritory(int territoryId)
        {
            return _territoryProvider.GetTerritoryById(territoryId);
        }

        public IEnumerable<Territory> GetAllTerritories()
        {
            return _territoryProvider.GetAllTerritories();
        }

        public IEnumerable<Territory> GetTerritoriesByGangId(string gangId)
        {
            var territories = _territoryProvider.GetTerritoryByGangId(gangId);

            foreach (var territory in territories)
            {
                territory.WorkTerritory = GetTerritoryEffect(territory.TerritoryId);
            }

            return territories;
        }

        public GangTerritory AddGangTerritory(GangTerritory gangTerritory)
        {
            return _territoryProvider.AddGangTerritory(gangTerritory);
        }

        public void RemoveGangTerritory(string gangTerritoryId)
        {
            _territoryProvider.RemoveGangTerritory(gangTerritoryId);
        }

        public Func<TerritoryWorkStatus, TerritoryIncomeReport> GetTerritoryEffect(int territoryId)
        {
            return _territoryEffects[(TerritoryEnum)territoryId];
        }

        private TerritoryIncomeReport NoTerritoryEffect(TerritoryWorkStatus status)
        {
            return new TerritoryIncomeReport()
            {
                Income = status.Roll
            };
        }

        private TerritoryIncomeReport ChemPit(TerritoryWorkStatus status)
        {
            if (status.Roll == 12)
            {
                // ganger gets horrible scars
                var scars = _injuryFactory.GetInjury((int)InjuryEnum.HORRIBLE_SCARS);
                status.Ganger = scars.InjuryEffect(status.Ganger);
                _gangerFactory.UpdateGanger(status.Ganger);

                return new TerritoryIncomeReport()
                {
                    Description = $"{status.Ganger.Name} has fallen into the chem pits and now has Horrible Scars. No income is collected.",
                    Income = 0
                };
            }
            return new TerritoryIncomeReport()
            {
                Income = 0
            };
        }

        private TerritoryIncomeReport Settlement(TerritoryWorkStatus status)
        {
            if (DiceRoller.RollDie() == 6)
            {
                // gang gets a free Juve.
                var juve = _gangerFactory.CreateJuve("New Juve");
                juve.GangId = status.GangId;
                _gangerFactory.UpdateGanger(juve);

                return new TerritoryIncomeReport()
                {
                    Description = "After working in the settlement, your gang has recruited a new Juve for free.",
                    Income = status.Roll
                };
            }
            return new TerritoryIncomeReport()
            {
                Income = status.Roll
            };
        }

        private TerritoryIncomeReport MineWorkings(TerritoryWorkStatus status)
        {
            // TODO: Right now we don't know which opponent gang members were captured, the opponent
            // would also have to submit a PostGameReport on Hivemind.
            return new TerritoryIncomeReport()
            {
                Income = status.Roll
            };
        }

        private TerritoryIncomeReport GuilderContract(TerritoryWorkStatus status)
        {
            if (status.PreviousBattleType == GameType.SCAVENGERS && status.Objectives > 0)
            {
                int extraIncome = status.Objectives * 5;
                return new TerritoryIncomeReport()
                {
                    Description = $"You have sold some of your extra loot to guilders for {extraIncome} credits.",
                    Income = status.Roll + extraIncome
                };
            }
            return new TerritoryIncomeReport()
            {
                Income = status.Roll
            };
        }

        private TerritoryIncomeReport FriendlyDoc(TerritoryWorkStatus status)
        {
            if (status.Deaths > 0)
            {
                int extraIncome = DiceRoller.RollDice(6, status.Deaths) * 5;
                return new TerritoryIncomeReport()
                {
                    Description = $"You have sold the bodies of your fallen gangers to the friendly doc for an extra {extraIncome} credits.",
                    Income = status.Roll + extraIncome
                };
            }
            return new TerritoryIncomeReport()
            {
                Income = status.Roll
            };
        }

        private TerritoryIncomeReport GamblingDen(TerritoryWorkStatus status)
        {
            // need to know the individual dice roll, so disregard what's in the status
            int roll1 = DiceRoller.RollDie();
            int roll2 = DiceRoller.RollDie();
            int income = (roll1 + roll2) * 10;

            if (roll1 == roll2)
            {
                return new TerritoryIncomeReport()
                {
                    Description = $"You've lost {income} credits at the gambling den.",
                    Income = income * -1
                };
            }

            return new TerritoryIncomeReport()
            {
                Income = income
            };
        }

        private TerritoryIncomeReport SporeCave(TerritoryWorkStatus status)
        {
            if (status.Roll == 20)
            {
                var sporeSickness = _injuryFactory.GetInjury((int)InjuryEnum.SPORE_SICKNESS);
                status.Ganger = sporeSickness.InjuryEffect(status.Ganger);
                _gangerFactory.UpdateGanger(status.Ganger);

                return new TerritoryIncomeReport()
                {
                    Description = $"{status.Ganger.Name} has contracted Spore Sickness while harvesting in the Spore Cave.",
                    Income = status.Roll
                };
            }
            return new TerritoryIncomeReport()
            {
                Income = status.Roll
            };
        }

        private TerritoryIncomeReport Archeotech(TerritoryWorkStatus status)
        {
            //TODO: need a good way to get a number of dice the player would like to use for archeotech harvesting.
            // If they roll any doubles, the archeotech becomes an old ruins.
            return new TerritoryIncomeReport()
            {
                Income = status.Roll
            };
        }
    }
}
