using Hivemind.Contracts;
using Hivemind.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hivemind.Entities
{
    public delegate TerritoryIncomeReport TerritoryEffect(TerritoryWorkStatus status);

    public class Territory: IComparable
    {
        public int TerritoryId { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Income { get; set; }
        public TerritoryEffect WorkTerritory { get; set; }


        /// <summary>
        /// Territories with higher IDs are generally better, so we should sort by descending ID.
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public int CompareTo(object obj)
        {
            if (obj.GetType() == typeof(Territory))
            {
                return this.TerritoryId - ((Territory)obj).TerritoryId;
            }
            throw new ArgumentException();
        }
    }
}
