using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Exceptions;
using NUnit.Framework;
using System.Collections.Generic;

namespace Hivemind.Tests.Entities
{
    [TestFixture]
    public class GangerTest
    {
        [TestCase]
        public void CreateGangerWithInvalidValues()
        {
            var ganger = new Ganger()
            {
                Name = "TestGanger",
                Move = -1,
                WeaponSkill = -1,
                BallisticSkill = -1,
                Strength = -1,
                Toughness = -1,
                Wounds = -1,
                Initiative = -1,
                Attack = -1,
                Leadership = -1
            };

            Assert.AreEqual(1, ganger.Move);
            Assert.AreEqual(1, ganger.WeaponSkill);
            Assert.AreEqual(1, ganger.BallisticSkill);
            Assert.AreEqual(1, ganger.Strength);
            Assert.AreEqual(1, ganger.Toughness);
            Assert.AreEqual(1, ganger.Wounds);
            Assert.AreEqual(1, ganger.Initiative);
            Assert.AreEqual(1, ganger.Attack);
            Assert.AreEqual(1, ganger.Leadership);
        }

        [TestCase(GangerType.Juve, new[] { 10 }, 35)]
        [TestCase(GangerType.Ganger, new[] { 15, 20 }, 85)]
        [TestCase(GangerType.Heavy, new[] { 10, 20 }, 90)]
        [TestCase(GangerType.Leader, new[] { 5, 10, 15 }, 150)]
        public void GetCostTest(GangerType gangerType, int[] weaponCosts, int expectedCost)
        {
            var ganger = new Ganger()
            {
                GangerType = gangerType,
            };

            var weapons = new List<GangerWeapon>();
            foreach (var cost in weaponCosts)
            {
                weapons.Add(new GangerWeapon()
                {
                    Cost = cost
                });
            }

            ganger.Weapons = weapons;
            ganger.GetCost();

            Assert.AreEqual(expectedCost, ganger.Cost);
        }

        [Test]
        public void InvalidGetCostTest()
        {
            var ganger = new Ganger()
            {
                GangerType = (GangerType)(-1),
            };

            Assert.Throws<HivemindException>(() => ganger.GetCost());
        }
    }
}
