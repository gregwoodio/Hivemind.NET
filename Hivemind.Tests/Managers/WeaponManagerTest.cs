using Hivemind.Entities;
using Hivemind.Enums;
using Hivemind.Managers;
using Hivemind.Managers.Implementation;
using Hivemind.Providers;
using Microsoft.Practices.Unity;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Tests.Managers
{
    [TestFixture]
    public class WeaponManagerTest
    {
        private IWeaponManager _weaponManager;

        public WeaponManagerTest()
        {
            _weaponManager = new WeaponManager(new WeaponProviderMock());
        }

        [TestCase]
        public void GetWeaponTest()
        {
            var result = _weaponManager.GetWeapon(1);
            var expected = new Weapon()
            {
                WeaponId = (WeaponEnum)1
            };

            Assert.AreEqual(expected.WeaponId, result.WeaponId);
        }

        [TestCase]
        public void GetAllWeaponsTest()
        {
            var result = _weaponManager.GetAllWeapons();

            Assert.AreEqual(3, result.Count());
            Assert.IsTrue(result.Any(w => w.WeaponId == (WeaponEnum)1));
            Assert.IsTrue(result.Any(w => w.WeaponId == (WeaponEnum)2));
            Assert.IsTrue(result.Any(w => w.WeaponId == (WeaponEnum)3));
        }

        [TestCase]
        public void GangWeaponsTest()
        {
            var gangId = "1";
            var gangWeapon = new GangWeapon()
            {
                GangId = gangId,
                Weapon = new Weapon()
                {
                    WeaponId = (WeaponEnum)1
                },
                Cost = 10
            };

            Assert.AreEqual(0, _weaponManager.GetGangStash(gangId).Count());

            var returnedWeapon = _weaponManager.AddGangWeapon(gangWeapon);
            Assert.AreEqual("DDDD-EEEE-FFFF", returnedWeapon.GangWeaponId);
            Assert.AreEqual(1, _weaponManager.GetGangStash(gangId).Count());

            _weaponManager.RemoveGangWeapon("DDDD-EEEE-FFFF");
            Assert.AreEqual(0, _weaponManager.GetGangStash(gangId).Count());
        }

        [TestCase]
        public void GangerWeaponsTest()
        {
            var gangerId = "1";
            var gangerWeapon = new GangerWeapon()
            {
                GangerId = gangerId,
                Weapon = new Weapon()
                {
                    WeaponId = (WeaponEnum)1
                },
                Cost = 10
            };

            Assert.AreEqual(0, _weaponManager.GetGangerWeapons(gangerId).Count());

            var returnedWeapon = _weaponManager.AddGangerWeapon(gangerWeapon);
            Assert.AreEqual("AAAA-BBBB-CCCC", returnedWeapon.GangerWeaponId);
            Assert.AreEqual(1, _weaponManager.GetGangerWeapons(gangerId).Count());

            _weaponManager.RemoveGangerWeapon("AAAA-BBBB-CCCC");
            Assert.AreEqual(0, _weaponManager.GetGangerWeapons(gangerId).Count());
        }

        [TestCase]
        public void GetGangerWeaponsByGangIdTest()
        {
            var result = _weaponManager.GetGangerWeaponsByGangId("1");
            Assert.NotNull(result);
        }
    }

    class WeaponProviderMock : IWeaponProvider
    {
        private List<Weapon> _weapons;
        private List<GangerWeapon> _gangerWeapons;
        private List<GangWeapon> _gangWeapons;

        public WeaponProviderMock()
        {
            _weapons = new List<Weapon>
            {
                new Weapon()
                {
                    WeaponId = (WeaponEnum)1
                },
                new Weapon()
                {
                    WeaponId = (WeaponEnum)2
                },
                new Weapon()
                {
                    WeaponId = (WeaponEnum)3
                },
            };

            _gangerWeapons = new List<GangerWeapon>();

            _gangWeapons = new List<GangWeapon>();
        }


        public GangerWeapon AddGangerWeapon(GangerWeapon gangerWeapon)
        {
            gangerWeapon.GangerWeaponId = "AAAA-BBBB-CCCC";
            _gangerWeapons.Add(gangerWeapon);

            return gangerWeapon;
        }

        public GangWeapon AddGangWeapon(GangWeapon gangWeapon)
        {
            gangWeapon.GangWeaponId = "DDDD-EEEE-FFFF";
            _gangWeapons.Add(gangWeapon);

            return gangWeapon;
        }

        public IEnumerable<Weapon> GetAllWeapons()
        {
            return _weapons;
        }

        public IEnumerable<GangerWeapon> GetByGangId(string gangId)
        {
            return _gangerWeapons;
        }

        public Weapon GetById(int weaponId)
        {
            return _weapons
                .Where(w => w.WeaponId == (WeaponEnum)weaponId)
                .FirstOrDefault();
        }

        public IEnumerable<GangerWeapon> GetGangerWeapons(string gangerId)
        {
            return _gangerWeapons.Where(gw => gw.GangerId == gangerId);
        }

        public IEnumerable<Weapon> GetGangerWeaponsByGangId(string gangId)
        {
            return _gangerWeapons.Select(gw => gw.Weapon);
        }

        public IEnumerable<Weapon> GetGangStash(string gangId)
        {
            return _gangWeapons.Select(gw => gw.Weapon);
        }

        public void RemoveGangerWeapon(string gangerWeaponId)
        {
            var gangerWeapon = _gangerWeapons.FirstOrDefault(gw => gw.GangerWeaponId == gangerWeaponId);
            _gangerWeapons.Remove(gangerWeapon);
        }

        public void RemoveGangWeapon(string gangWeaponId)
        {
            var gangWeapon = _gangWeapons.FirstOrDefault(gw => gw.GangWeaponId == gangWeaponId);
            _gangWeapons.Remove(gangWeapon);
        }
    }
}
