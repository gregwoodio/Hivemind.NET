using Hivemind.Entities;
using Hivemind.Enums;
using System.Collections.Generic;

namespace Hivemind.Entities
{
    public class Gang
    {
        public string GangId { get; set; }
        public string Name { get; set; }
        public int Credits { get; set; }
        public GangHouse GangHouse { get; set; }
        public IEnumerable<Ganger> Gangers { get; set; }
        public IEnumerable<GangTerritory> Territories { get; set; }

        public Gang()
        {
            Credits = 1000;
            Gangers = new List<Ganger>();
            Territories = new List<GangTerritory>();
        }

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