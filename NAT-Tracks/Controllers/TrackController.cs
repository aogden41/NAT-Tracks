using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using Microsoft.AspNetCore.Mvc;
using NAT_Tracks.Models;
using System.Net;

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
            //Redirect to usage page on ganderoceanic.com
            return Redirect("https://ganderoceanic.com/nat-track-api-usage");
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

        /// <summary>
        /// Get all CTP tracks or single CTP track
        /// </summary>
        /// <returns>All NAT Tracks as Track objects</returns>
        [HttpGet]
        [Route("/event")]
        public ContentResult GetEvent(string id = null)
        {
            // CTP path
            string path = "https://cdn.ganderoceanic.com/resources/data/eventTracks.json";

            // Return
            using (WebClient client = new WebClient())
            {
                return Content(client.DownloadString(path));
            }
        }
    }
}
