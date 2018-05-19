using Hivemind.Entities;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Tests.Entities
{
    [TestFixture]
    public class TerritoryTest
    {
        [TestCase]
        public void CompareToTest()
        {
            var territory1 = new Territory()
            {
                TerritoryId = 1
            };

            var territory2 = new Territory()
            {
                TerritoryId = 2
            };

            Assert.AreEqual(-1, territory1.CompareTo(territory2));
            Assert.AreEqual(1, territory2.CompareTo(territory1));
            Assert.Throws<ArgumentException>(() => territory1.CompareTo(new object()));
        }
    }
}
