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

        public string Route { get; set; }

        public string FlightLevels { get; set; }

        public string Direction { get; set; }
    }
}
