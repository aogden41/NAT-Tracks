using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using NAT_Tracks.Models;

namespace NAT_Tracks.Controllers
{
    [Route("api/track")]
    public class TrackController : Controller
    {
        // GET api/track
        [HttpGet]
        public IEnumerable<Track> Get()
        {
            // Return all tracks
            return Utils.ParseTracks();
        }

        // GET api/track/B
        [HttpGet("{id}")]
        public JsonResult Get(char id)
        {
            // Umm this works?
            id = id.ToString().ToUpper().ToCharArray()[0];

            // Return specific track
            return Json(Utils.ParseTracks().Where(t => t.Id == id).FirstOrDefault());
        }
    }
}
