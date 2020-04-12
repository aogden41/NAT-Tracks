using System.Collections.Generic;
using System.Linq;
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
            return "To fetch all tracks, type '/api/tracks' in the URL bar. To fetch a specific track, type '/api/track/[track identifier]' in the URL bar.";
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
