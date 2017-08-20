using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Exceptions
{
    public class HivemindException : Exception
    {
        public HivemindException(string error) : base(error) { }

        public static HivemindException GangerNotFoundException(int id)
        {
            throw new HivemindException($"Ganger with id '{id}' not found.");
        }

        public static HivemindException NoSuchInjuryException(int id)
        {
            throw new HivemindException($"No injury with id '{id}' found.");
        }

        public static HivemindException NoSuchInjuryException(string name)
        {
            throw new HivemindException($"No injury with name '{name}' found.");
        }
    }
}
