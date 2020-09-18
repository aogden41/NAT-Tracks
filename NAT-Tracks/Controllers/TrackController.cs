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
        [Route("/")]
        public string Index()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("NAT Track API\n\nUsage:\n");
            sb.Append("Get all tracks: /data\n");
            sb.Append("Get all tracks (altitude as metres): /data?si=true\n");
            sb.Append("Get single track: /data?id={track ID} (eg: /data?id=a)\n");
            sb.Append("Get single track (altitude as metres): /data?id={track ID}&si=true (eg: /data?id=a&si=true)\n\n");
            sb.Append("GitHub: https://github.com/andrewogden1678/NAT-Tracks");

            return sb.ToString();
        }

        /// <summary>
        /// Get all tracks or single track
        /// </summary>
        /// <param name="metres">SI Units?</param>
        /// <returns>All NAT Tracks as Track objects</returns>
        [HttpGet]
        [Route("/data")]
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
