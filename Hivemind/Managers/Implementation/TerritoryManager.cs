// <copyright file="TerritoryManager.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using Hivemind.Contracts;
using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Providers;
using Hivemind.Utilities;

namespace Hivemind.Managers.Implementation
{
    /// <summary>
    /// Territory manager
    /// </summary>
    public class TerritoryManager : ITerritoryManager
    {
        private IInjuryManager _injuryManager;
        private IGangerManager _gangerManager;
        private ITerritoryProvider _territoryProvider;
        private Dictionary<TerritoryEnum, Func<TerritoryWorkStatus, TerritoryIncomeReport>> _territoryEffects;

        /// <summary>
        /// Initializes a new instance of the <see cref="TerritoryManager"/> class.
        /// </summary>
        /// <param name="injuryManager">Injury manager</param>
        /// <param name="gangerManager">Ganger manager</param>
        /// <param name="territoryProvider">Territory provider</param>
        public TerritoryManager(IInjuryManager injuryManager, IGangerManager gangerManager, ITerritoryProvider territoryProvider)
        {
            _injuryManager = injuryManager ?? throw new ArgumentNullException(nameof(injuryManager));
            _gangerManager = gangerManager ?? throw new ArgumentNullException(nameof(gangerManager));
            _territoryProvider = territoryProvider ?? throw new ArgumentNullException(nameof(territoryProvider));

            _territoryEffects = new Dictionary<TerritoryEnum, Func<TerritoryWorkStatus, TerritoryIncomeReport>>
            {
                { TerritoryEnum.Chempit, ChemPit },
                { TerritoryEnum.OldRuins, NoTerritoryEffect },
                { TerritoryEnum.Slag, NoTerritoryEffect },
                { TerritoryEnum.MineralOutcrop, NoTerritoryEffect },
                { TerritoryEnum.Settlement, Settlement },
                { TerritoryEnum.MineWorkings, MineWorkings },
                { TerritoryEnum.Tunnels, NoTerritoryEffect },
                { TerritoryEnum.Vents, NoTerritoryEffect },
                { TerritoryEnum.Holestead, NoTerritoryEffect },
                { TerritoryEnum.Waterstill, NoTerritoryEffect },
                { TerritoryEnum.DrinkingHole, NoTerritoryEffect },
                { TerritoryEnum.GuilderContract, GuilderContract },
                { TerritoryEnum.FriendlyDoc, FriendlyDoc },
                { TerritoryEnum.Workshop, NoTerritoryEffect },
                { TerritoryEnum.GamblingDen, GamblingDen },
                { TerritoryEnum.SporeCave, SporeCave },
                { TerritoryEnum.Archeotech, Archeotech },
                { TerritoryEnum.GreenHivers, NoTerritoryEffect },
            };
        }

        /// <summary>
        /// Get Territory
        /// </summary>
        /// <param name="territoryId">Territory ID</param>
        /// <returns>Territory</returns>
        public Territory GetTerritory(int territoryId)
        {
            return _territoryProvider.GetTerritoryById(territoryId);
        }

        /// <summary>
        /// Get all territories
        /// </summary>
        /// <returns>All territories</returns>
        public IEnumerable<Territory> GetAllTerritories()
        {
            return _territoryProvider.GetAllTerritories();
        }

        /// <summary>
        /// Get territories by gang ID
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>Territories owned by a gang</returns>
        public IEnumerable<Territory> GetTerritoriesByGangId(string gangId)
        {
            var territories = _territoryProvider.GetTerritoryByGangId(gangId);

            foreach (var territory in territories)
            {
                territory.WorkTerritory = GetTerritoryEffect(territory.TerritoryId);
            }

            return territories;
        }

        /// <summary>
        /// Add gang territory
        /// </summary>
        /// <param name="gangTerritory">Gang territory</param>
        /// <returns>Added gang territory</returns>
        public GangTerritory AddGangTerritory(GangTerritory gangTerritory)
        {
            return _territoryProvider.AddGangTerritory(gangTerritory);
        }

        /// <summary>
        /// Remove gang territory
        /// </summary>
        /// <param name="gangTerritoryId">Gang territory ID</param>
        public void RemoveGangTerritory(string gangTerritoryId)
        {
            _territoryProvider.RemoveGangTerritory(gangTerritoryId);
        }

        /// <summary>
        /// Get territory effect, representing the actions that occur when working a territory.
        /// </summary>
        /// <param name="territoryId">Territory ID</param>
        /// <returns>Function that makes changes based on territory worked.</returns>
        public Func<TerritoryWorkStatus, TerritoryIncomeReport> GetTerritoryEffect(int territoryId)
        {
            return _territoryEffects[(TerritoryEnum)territoryId];
        }

        #region Territory effects
        private TerritoryIncomeReport NoTerritoryEffect(TerritoryWorkStatus status)
        {
            return new TerritoryIncomeReport()
            {
                TerritoryName = status.TerritoryName,
                Income = status.Roll,
            };
        }

        private TerritoryIncomeReport ChemPit(TerritoryWorkStatus status)
        {
            if (status.Roll == 12)
            {
                // ganger gets horrible scars
                var scars = _injuryManager.GetInjury((int)InjuryEnum.HorribleScars);
                status.Ganger = scars.InjuryEffect(status.Ganger);
                _gangerManager.UpdateGanger(status.Ganger);

                return new TerritoryIncomeReport()
                {
                    TerritoryName = status.TerritoryName,
                    Description = $"{status.Ganger.Name} has fallen into the chem pits and now has Horrible Scars. No income is collected.",
                    Income = 0,
                };
            }

            return new TerritoryIncomeReport()
            {
                TerritoryName = status.TerritoryName,
                Income = 0,
            };
        }

        private TerritoryIncomeReport Settlement(TerritoryWorkStatus status)
        {
            if (DiceRoller.RollDie() == 6)
            {
                // gang gets a free Juve.
                var juve = _gangerManager.CreateJuve("New Juve");
                juve.GangId = status.GangId;
                _gangerManager.AddGanger(juve);

                return new TerritoryIncomeReport()
                {
                    TerritoryName = status.TerritoryName,
                    Description = "After working in the settlement, your gang has recruited a new Juve for free.",
                    Income = status.Roll,
                };
            }

            return new TerritoryIncomeReport()
            {
                TerritoryName = status.TerritoryName,
                Income = status.Roll,
            };
        }

        private TerritoryIncomeReport MineWorkings(TerritoryWorkStatus status)
        {
            // TODO: Right now we don't know which opponent gang members were captured, the opponent
            // would also have to submit a PostGameReport on Hivemind.
            return new TerritoryIncomeReport()
            {
                TerritoryName = status.TerritoryName,
                Income = status.Roll,
            };
        }

        private TerritoryIncomeReport GuilderContract(TerritoryWorkStatus status)
        {
            if (status.PreviousBattleType == GameType.Scavengers && status.Objectives > 0)
            {
                int extraIncome = status.Objectives * 5;
                return new TerritoryIncomeReport()
                {
                    TerritoryName = status.TerritoryName,
                    Description = $"You have sold some of your extra loot to guilders for {extraIncome} credits.",
                    Income = status.Roll + extraIncome,
                };
            }

            return new TerritoryIncomeReport()
            {
                TerritoryName = status.TerritoryName,
                Income = status.Roll,
            };
        }

        private TerritoryIncomeReport FriendlyDoc(TerritoryWorkStatus status)
        {
            if (status.Deaths > 0)
            {
                int extraIncome = DiceRoller.RollDice(6, status.Deaths) * 5;
                return new TerritoryIncomeReport()
                {
                    TerritoryName = status.TerritoryName,
                    Description = $"You have sold the bodies of your fallen gangers to the friendly doc for an extra {extraIncome} credits.",
                    Income = status.Roll + extraIncome,
                };
            }

            return new TerritoryIncomeReport()
            {
                TerritoryName = status.TerritoryName,
                Income = status.Roll,
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
                    TerritoryName = status.TerritoryName,
                    Description = $"You've lost {income} credits at the gambling den.",
                    Income = income * -1,
                };
            }

            return new TerritoryIncomeReport()
            {
                TerritoryName = status.TerritoryName,
                Income = income,
            };
        }

        private TerritoryIncomeReport SporeCave(TerritoryWorkStatus status)
        {
            if (status.Roll == 20)
            {
                var sporeSickness = _injuryManager.GetInjury((int)InjuryEnum.SporeSickness);
                status.Ganger = sporeSickness.InjuryEffect(status.Ganger);
                _gangerManager.UpdateGanger(status.Ganger);

                return new TerritoryIncomeReport()
                {
                    TerritoryName = status.TerritoryName,
                    Description = $"{status.Ganger.Name} has contracted Spore Sickness while harvesting in the Spore Cave.",
                    Income = status.Roll,
                };
            }

            return new TerritoryIncomeReport()
            {
                TerritoryName = status.TerritoryName,
                Income = status.Roll,
            };
        }

        private TerritoryIncomeReport Archeotech(TerritoryWorkStatus status)
        {
            // TODO: need a good way to get a number of dice the player would like to use for archeotech harvesting.
            // If they roll any doubles, the archeotech becomes an old ruins.
            return new TerritoryIncomeReport()
            {
                TerritoryName = status.TerritoryName,
                Income = status.Roll,
            };
        }
        #endregion
    }
}
