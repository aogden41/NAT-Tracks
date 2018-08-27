using Microsoft.EntityFrameworkCore.Internal;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace NAT_Tracks.Models
{
    public static class Utils
    {
        private static string _trackUrl = "https://pilotweb.nas.faa.gov/common/nat.html";

        public static List<Track> ParseTracks()
        {
            // Define content variable to store data from webpage
            string content = string.Empty;

            // Handle the request
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(_trackUrl);

            // Handle the response
            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            Stream responseStream = response.GetResponseStream();

            // Read file
            using (StreamReader reader = new StreamReader(responseStream))
            {
                content = reader.ReadToEnd();
            }

            // Parse string into a list of strings
            List<string> splitList = content.Split("\n").ToList();

            // Parse even further
            List<List<string>> parsedList = new List<List<string>>();

            // Dodgy af but it works :)
            int getNextFour = 0;

            // Track
            List<string> track = new List<string>();

            // Get the tracks and their data
            for (int i = 0; i < splitList.Count; i++)
            {
                try
                {
                    // If the row is a track routing
                    if ((Char.IsLetter(splitList[i][0]) && Char.IsWhiteSpace(splitList[i][1])) || getNextFour > 0)
                    {
                        // Start incrementing to get the next four rows
                        getNextFour++;

                        // Check if greater than four
                        if (getNextFour > 4)
                        {
                            // Start over
                            getNextFour = 0;
                            parsedList.Add(track);
                            track = new List<string>();
                            continue;
                        }

                        // Add to the track
                        track.Add(splitList[i]);
                    }
                }
                catch (Exception ex) // Catch any exception
                {
                    break;
                }
            }

            // Final list to return
            List<Track> returnList = new List<Track>();

            // Build track objects
            foreach(List<string> list in parsedList)
            {
                // Direction & Flight levels
                string direction = string.Empty;
                string flightLevels = string.Empty;
                if (list[1].Contains("EAST LVLS NIL"))
                {
                    direction = "WEST";
                    flightLevels = list[2].Remove(0, 10);
                } 
                else
                {
                    direction = "EAST";
                    flightLevels = list[1].Remove(0, 10);
                }

                // Build new track object
                Track trackObj = new Track
                {
                    Id = list[0][0],
                    TMI = DateTime.Now.DayOfYear,
                    Route = list[0].Remove(0, 2),
                    Direction = direction,
                    FlightLevels = flightLevels
                };

                returnList.Add(trackObj);
            }

            return returnList;
        }
    }
}
