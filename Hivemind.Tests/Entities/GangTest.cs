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
    public class GangTest
    {
        [TestCase]
        public void GangRatingTest()
        {
            var gang = new Gang()
            {
                Gangers = new[]
                {
                    new Ganger()
                    {
                        Experience = 50,
                        Cost = 50
                    },
                    new Ganger()
                    {
                        Experience = 50,
                        Cost = 50
                    },
                    new Ganger()
                    {
                        Experience = 50,
                        Cost = 50
                    },
                    new Ganger()
                    {
                        Experience = 50,
                        Cost = 50
                    }
                }
            };

            var expected = 400;
            Assert.AreEqual(expected, gang.GangRating);
        }
    }
}
