using Hivemind.Exceptions;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Tests.Exceptions
{
    [TestFixture]
    public class HivemindExceptionTests
    {
        [TestCase]
        public void ExceptionsTest()
        {
            Assert.Throws<HivemindException>(() => HivemindException.GangerNotFoundException(-1));
            Assert.Throws<HivemindException>(() => HivemindException.NoSuchInjuryException(-1));
            Assert.Throws<HivemindException>(() => HivemindException.NoSuchGangHouse());
            Assert.Throws<HivemindException>(() => HivemindException.InvalidUsernameOrPassword());
        }
    }
}
