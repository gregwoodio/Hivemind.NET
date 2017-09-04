using Hivemind.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Factories
{
    public interface IGangFactory
    {
        Gang GetGang(string gangId);
        Gang UpdateGang(Gang gang);
        Gang AddGang(Gang gang);
    }
}
