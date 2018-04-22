// <copyright file="StoreService.cs" company="weirdvector">
// Copyright (c) weirdvector. All rights reserved.
// </copyright>

using System;
using System.Collections.Generic;
using System.Linq;
using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Exceptions;
using Hivemind.Managers;
using Hivemind.Utilities;

namespace Hivemind.Services.Implementation
{
    /// <summary>
    /// Store service
    /// </summary>
    public class StoreService : IStoreService
    {
        private IWeaponManager _weaponManager;
        private IGangManager _gangManager;
        private IDiceRoller _diceRoller;

        /// <summary>
        /// Initializes a new instance of the <see cref="StoreService"/> class.
        /// </summary>
        /// <param name="weaponManager">Weapon manager</param>
        /// <param name="diceRoller">Dice roller</param>
        public StoreService(IWeaponManager weaponManager, IGangManager gangManager, IDiceRoller diceRoller)
        {
            _weaponManager = weaponManager ?? throw new ArgumentNullException(nameof(weaponManager));
            _gangManager = gangManager ?? throw new ArgumentNullException(nameof(gangManager));
            _diceRoller = diceRoller ?? throw new ArgumentNullException(nameof(diceRoller));
        }

        /// <summary>
        /// Buy equipment
        /// </summary>
        /// <param name="equipment">Equipment</param>
        public void BuyEquipment(GangWeapon equipment)
        {
            // TODO: Add some validation to ensure cost is correct.
            _gangManager.Spend(equipment.GangId, equipment.Cost);
            _weaponManager.AddGangWeapon(equipment);
        }

        /// <summary>
        /// Buy multiple equipment items
        /// </summary>
        /// <param name="equipment">Equipment</param>
        public void BuyEquipment(IEnumerable<GangWeapon> equipment)
        {
            foreach (var equipmentItem in equipment)
            {
                BuyEquipment(equipmentItem);
            }
        }

        /// <summary>
        /// Gets list of commonly available equipment
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>Common equipment list</returns>
        public IEnumerable<GangWeapon> GetCommonEquipment(string gangId)
        {
            return _weaponManager.GetAllWeapons()
                .Where(weapon => weapon.WeaponAvailability == WeaponAvailability.Common)
                .Select(weapon => new GangWeapon()
                {
                    GangId = gangId,
                    Cost = _diceRoller.ParseDiceString(weapon.Cost),
                    Weapon = weapon,
                });
        }

        /// <summary>
        /// Gets list of rare equipment currently available
        /// </summary>
        /// <param name="gangId">Gang ID</param>
        /// <returns>Rare equipment list</returns>
        public IEnumerable<GangWeapon> GetRareEquipment(string gangId)
        {
            // TODO: A player can send more gangers out to search for rare items. Should these be
            // gangers not sent to work a territory?
            var gangersSent = 0;
            var rareItemsFound = _diceRoller.RollDice(3, 1) + gangersSent;

            var rareItems = new List<GangWeapon>();

            while (rareItemsFound > 0)
            {
                var weapon = RareItemsRoll();
                rareItems.Add(new GangWeapon()
                {
                    GangId = gangId,
                    Weapon = weapon,
                    Cost = _diceRoller.ParseDiceString(weapon.Cost),
                });
                rareItemsFound--;
            }

            return rareItems;
        }

        private Weapon RareItemsRoll()
        {
            var now = DateTime.Now;
            var seed = new DateTime(now.Year, now.Month, now.Day, now.Hour, 0, 0).Ticks;
            var roll = _diceRoller.RollD66((int)seed);

            switch (roll)
            {
                case 11:
                case 12:
                case 13:
                case 14:
                    return PowerWeaponRoll();
                case 15:
                    return RareWeaponRoll();
                case 16:
                    return GasGrenadesRoll();
                case 21:
                case 22:
                    return GrenadesRoll();
                case 23:
                case 24:
                    return _weaponManager.GetWeapon((int)WeaponEnum.HotshotLaserPowerPack);
                case 25:
                case 26:
                case 31:
                    return GunsightRoll();
                case 32:
                case 33:
                case 34:
                    return ArmourRoll();
                case 35:
                    return BionicsRoll();
                case 36:
                    return _weaponManager.GetWeapon((int)WeaponEnum.AutoRepairer);
                case 41:
                    return _weaponManager.GetWeapon((int)WeaponEnum.BioBooster);
                case 42:
                    return _weaponManager.GetWeapon((int)WeaponEnum.BioScanner);
                case 43:
                    return _weaponManager.GetWeapon((int)WeaponEnum.BlindsnakePouch);
                case 44:
                    return _weaponManager.GetWeapon((int)WeaponEnum.ConcealedBlade);
                case 45:
                    return _weaponManager.GetWeapon((int)WeaponEnum.GravChute);
                case 46:
                    return _weaponManager.GetWeapon((int)WeaponEnum.Grapnel);
                case 51:
                    return _weaponManager.GetWeapon((int)WeaponEnum.InfraRedGoggles);
                case 52:
                    // It's an inanimate carbon rod!
                    return _weaponManager.GetWeapon((int)WeaponEnum.IsotropicFuelRod);
                case 53:
                case 54:
                    return _weaponManager.GetWeapon((int)WeaponEnum.MediPack);
                case 55:
                    return _weaponManager.GetWeapon((int)WeaponEnum.MungVase);
                case 56:
                    return _weaponManager.GetWeapon((int)WeaponEnum.RatskinMap);
                case 61:
                    return _weaponManager.GetWeapon((int)WeaponEnum.Screamers);
                case 62:
                    return _weaponManager.GetWeapon((int)WeaponEnum.SkullChip);
                case 63:
                    return _weaponManager.GetWeapon((int)WeaponEnum.Silencer);
                case 64:
                    return _weaponManager.GetWeapon((int)WeaponEnum.Stummers);
                case 65:
                case 66:
                    return _weaponManager.GetWeapon((int)WeaponEnum.WeaponReloads);
                default:
                    throw new HivemindException("Invalid dice roll");
            }
        }

        private Weapon BionicsRoll()
        {
            var roll = _diceRoller.RollDie();
            switch (roll)
            {
                case 1:
                case 2:
                    return _weaponManager.GetWeapon((int)WeaponEnum.BionicArm);
                case 3:
                case 4:
                    return _weaponManager.GetWeapon((int)WeaponEnum.BionicEye);
                case 5:
                case 6:
                    return _weaponManager.GetWeapon((int)WeaponEnum.BionicLeg);
                default:
                    throw new HivemindException("Invalid dice roll");
            }
        }

        private Weapon ArmourRoll()
        {
            var roll = _diceRoller.RollDie();
            switch (roll)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    return _weaponManager.GetWeapon((int)WeaponEnum.FlakArmor);
                case 5:
                    return _weaponManager.GetWeapon((int)WeaponEnum.CarapaceArmor);
                case 6:
                    return _weaponManager.GetWeapon((int)WeaponEnum.MeshArmor);
                default:
                    throw new HivemindException("Invalid dice roll");
            }
        }

        private Weapon GunsightRoll()
        {
            var roll = _diceRoller.RollDie();
            switch (roll)
            {
                case 1:
                case 2:
                    return _weaponManager.GetWeapon((int)WeaponEnum.RedDotLaserSight);
                case 3:
                    return _weaponManager.GetWeapon((int)WeaponEnum.MonoSight);
                case 4:
                    return _weaponManager.GetWeapon((int)WeaponEnum.TelescopicSight);
                case 5:
                case 6:
                    return _weaponManager.GetWeapon((int)WeaponEnum.InfraRedSight);
                default:
                    throw new HivemindException("Invalid dice roll");
            }
        }

        private Weapon GrenadesRoll()
        {
            var roll = _diceRoller.RollDie();
            switch (roll)
            {
                case 1:
                    return _weaponManager.GetWeapon((int)WeaponEnum.MeltaBomb);
                case 2:
                case 3:
                    return _weaponManager.GetWeapon((int)WeaponEnum.PhotonFlashFlare);
                case 4:
                    return _weaponManager.GetWeapon((int)WeaponEnum.PlasmaGrenade);
                case 5:
                case 6:
                    return _weaponManager.GetWeapon((int)WeaponEnum.SmokeGrenade);
                default:
                    throw new HivemindException("Invalid dice roll");
            }
        }

        private Weapon GasGrenadesRoll()
        {
            var roll = _diceRoller.RollDie();
            switch (roll)
            {
                case 1:
                case 2:
                    return _weaponManager.GetWeapon((int)WeaponEnum.ChokeGasGrenade);
                case 3:
                case 4:
                    return _weaponManager.GetWeapon((int)WeaponEnum.ScareGasGrenade);
                case 5:
                case 6:
                    return _weaponManager.GetWeapon((int)WeaponEnum.HallucinogenGasGrenade);
                default:
                    throw new HivemindException("Invalid dice roll");
            }
        }

        private Weapon RareWeaponRoll()
        {
            var roll = _diceRoller.RollDie();
            switch (roll)
            {
                case 1:
                case 2:
                case 3:
                    return NeedleWeaponRoll();
                case 4:
                case 5:
                    return _weaponManager.GetWeapon((int)WeaponEnum.WebPistol);
                case 6:
                    return _weaponManager.GetWeapon((int)WeaponEnum.OneInAMillionWeapon);
                default:
                    throw new HivemindException("Invalid dice roll");
            }
        }

        private Weapon NeedleWeaponRoll()
        {
            var roll = _diceRoller.RollDie();
            switch (roll)
            {
                case 1:
                case 2:
                case 3:
                case 4:
                    return _weaponManager.GetWeapon((int)WeaponEnum.NeedlePistol);
                case 5:
                case 6:
                    return _weaponManager.GetWeapon((int)WeaponEnum.NeedleRifle);
                default:
                    throw new HivemindException("Invalid dice roll");
            }
        }

        private Weapon PowerWeaponRoll()
        {
            var roll = _diceRoller.RollDie();
            switch (roll)
            {
                case 1:
                    return _weaponManager.GetWeapon((int)WeaponEnum.PowerAxeOneHanded);
                case 2:
                    return _weaponManager.GetWeapon((int)WeaponEnum.PowerFist);
                case 3:
                    return _weaponManager.GetWeapon((int)WeaponEnum.PowerMaul);
                case 4:
                case 5:
                case 6:
                    return _weaponManager.GetWeapon((int)WeaponEnum.PowerSword);
                default:
                    throw new HivemindException("Invalid dice roll");
            }
        }
    }
}
