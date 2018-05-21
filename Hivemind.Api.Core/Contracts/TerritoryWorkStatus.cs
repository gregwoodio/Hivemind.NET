using Hivemind.Entities;
using Hivemind.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Contracts
{
    public class TerritoryWorkStatus
    {
        public string TerritoryName;
        public string GangId;
        public Ganger Ganger;
        public GameType PreviousBattleType;
        public int Deaths;
        public int Objectives;
        public int Roll;
    }
}
