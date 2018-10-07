using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAT_Tracks.Models
{
    public class Track
    {
        public char Id { get; set; }

        public int TMI { get; set; }

        public List<string> Route { get; set; }

        public List<int> FlightLevels { get; set; }

        public Direction Direction { get; set; }
    }

    public enum Direction
    {
        UNKNOWN,
        WEST,
        EAST
    }
}
