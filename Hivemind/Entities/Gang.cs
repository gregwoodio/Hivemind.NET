using Hivemind.Entities;
using Hivemind.Enums;
using System.Collections.Generic;

namespace Hivemind.Entities
{
    public class Gang
    {
        public int GangId { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public GangHouse House { get; set; }
        public IEnumerable<Ganger> Gangers { get; set; }
        public int GangRating
        {
            get
            {
                int rating = 0;
                foreach (var ganger in Gangers)
                {
                    rating += ganger.Cost + ganger.Experience;
                }
                return rating;
            }
        }
    }
}