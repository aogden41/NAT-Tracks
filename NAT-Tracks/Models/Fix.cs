using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace NAT_Tracks.Models
{
    /// <summary>
    /// NAT fix (including coordinates)
    /// </summary>
    public class Fix
    {
        /// <summary>
        /// Name of fix
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Latitude of fix
        /// </summary>
        public double Latitude { get; set; }

        /// <summary>
        /// Longitude of fix
        /// </summary>
        public double Longitude { get; set; }

        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="name">Name</param>
        /// <param name="lat">Latitude</param>
        /// <param name="lon">Longitude</param>
        public Fix(string name, double lat = 0.0, double lon = 0.0)
        {
            this.Name = name;
            this.Latitude = lat;
            this.Longitude = lon;
        }
    }
}
