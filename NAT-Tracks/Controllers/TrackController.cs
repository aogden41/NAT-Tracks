using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using NAT_Tracks.Models;

namespace NAT_Tracks.Controllers
{
    [Route("api/tracks/")]
    public class TrackController : Controller
    {
        /// <summary>
        /// Index page
        /// </summary>
        /// <returns>API instructions</returns>
        [HttpGet]
        [Route("/api")]
        public string Index()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("NAT Track API\n\nUsage:\n");
            sb.Append("Get all tracks: /api/tracks\n");
            sb.Append("Get all tracks (altitude as metres): /api/tracks?si=true\n");
            sb.Append("Get single track: /api/tracks?id={track ID} (eg: /api/tracks?id=a)\n");
            sb.Append("Get single track (altitude as metres): /api/tracks?id={track ID}&si=true\n\n");
            sb.Append("Author: Andrew Ogden (1336925)");

            return sb.ToString();
        }

        /// <summary>
        /// Get all tracks (api/tracks/{si?}) or single track (api/tracks/{id?}/{si?})
        /// </summary>
        /// <param name="metres">SI Units?</param>
        /// <returns>All NAT Tracks as Track objects</returns>
        [HttpGet]
        public JsonResult Get(string id = null, bool si = false)
        {
            // Return specific track
            if (id != null)
            {
                char charID = id.ToString().ToUpper().ToCharArray()[0];
                
                if (si) return Json(Utils.ParseTracks(si).Where(t => t.Id == charID).FirstOrDefault());

                else return Json(Utils.ParseTracks().Where(t => t.Id == charID).FirstOrDefault());
            }

            // Return metres
            if (si) return Json(Utils.ParseTracks(si));

            else return Json(Utils.ParseTracks());
        }
    }
}
