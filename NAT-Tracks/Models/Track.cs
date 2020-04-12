using System.Collections.Generic;

namespace NAT_Tracks.Models
{
    /// <summary>
    /// NAT Track
    /// </summary>
    public class Track
    {
        /// <summary>
        /// Track ID
        /// </summary>
        public char Id { get; set; }

        /// <summary>
        /// Track Message Identifier
        /// </summary>
        public string TMI { get; set; }

        /// <summary>
        /// Route (list of fix objects)
        /// </summary>
        public List<Fix> Route { get; set; }

        /// <summary>
        /// List of flight levels (metres or feet)
        /// </summary>
        public List<int> FlightLevels { get; set; }

        /// <summary>
        /// Direction of traffic flow (enum value)
        /// </summary>
        public Direction Direction { get; set; }
    }

    /// <summary>
    /// Direction of traffic flow
    /// </summary>
    public enum Direction
    {
        UNKNOWN,
        WEST,
        EAST
    }
}
