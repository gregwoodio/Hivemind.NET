using Hivemind.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Contracts
{
    /// <summary>
    /// Returned from the web console after a match. Should contain all information about how the gangers performed during
    /// the match, as well as which gang was used, whether they won, and the opponent gang rating.
    /// </summary>
    public class BattleReport
    {
        public string GangId { get; set; }
        public IEnumerable<GangerBattleStats> GangBattleStats { get; set; }
        public bool HasWon { get; set; }
        public bool IsAttacker { get; set; }
        public int OpponentGangRating { get; set; }
        public GameType GameType { get; set; }
    }
}
