using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace NAT_Tracks.Models
{
    public static class Utils
    {
        // Static URLs
        private const string _trackUrl = "https://www.notams.faa.gov/common/nat.html";
        private const string _fixesJson = "https://resources.ganderoceanic.com/data/fixes.json";

        public static List<Track> ParseTracks(bool isMetres = false)
        {
            // NAT tracks
            string content = new WebClient().DownloadString(_trackUrl);

            // String of waypoint fixes & coords
            string fixesDataJson = new WebClient().DownloadString(_fixesJson);

            // Parse string into a list of strings
            List<string> splitList = content.Split("\n").ToList();

            // Parse even further
            List<List<string>> parsedList = new List<List<string>>();

            // Dodgy but it works :)
            int getNextFour = 0;

            // Track
            List<string> track = new List<string>();

            string tmi = string.Empty;

            // Get the tracks and their data
            for (int i = 0; i < splitList.Count; i++)
            {
                try
                {
                    // If the row is a track routing
                    if ((char.IsLetter(splitList[i][0]) && char.IsWhiteSpace(splitList[i][1])) || getNextFour > 0)
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
                    else if (splitList[i].Contains("TMI IS")) 
                    {
                        tmi = Convert.ToString(splitList[i][10]) + Convert.ToString(splitList[i][11]) + Convert.ToString(splitList[i][12]);

                        // Add amendment character if exists
                        if (char.IsLetter(splitList[i][13])) tmi += splitList[i][13];
                    }
                }
                catch (Exception) // Catch any exception
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
                Direction direction = Direction.UNKNOWN;
                List<int> flightLevels = new List<int>();

                // If no east levels
                if (list[1].Trim().ToUpper().Contains("EAST LVLS NIL"))
                {
                    direction = Direction.WEST;

                    // Split into list of strings
                    List<string> rawFlightLevels = list[2].Remove(0, 10).Split(" ").ToList();
                    foreach (string fl in rawFlightLevels)
                    {
                        if (isMetres)
                        {
                            flightLevels.Add(Convert.ToInt32((int.Parse(fl) * 100) / 3.2808));
                        }
                        else
                        {
                            flightLevels.Add(int.Parse(fl) * 100);
                        }
                    }
                } 
                else
                {
                    direction = Direction.EAST;

                    // Split into list of strings
                    List<string> rawFlightLevels = list[1].Remove(0, 10).Split(" ").ToList();
                    foreach (string fl in rawFlightLevels)
                    {
                        if (isMetres)
                        {
                            flightLevels.Add(Convert.ToInt32((int.Parse(fl) * 100) / 3.2808));
                        }
                        else 
                        {
                            flightLevels.Add(int.Parse(fl) * 100);
                        }                        
                    }
                }

                // Translate route into decimal coordinates
                List<string> routeCoords = list[0].Remove(0, 2).Split(" ").ToList();
                List<Fix> finalRoute = new List<Fix>();
                foreach(string point in routeCoords)
                {
                    if (point.Contains("/")) // If it is a coordinate
                    {
                        // Split the coordinates
                        string[] newPoint = point.Split("/");

                        // Parse lat/lon to a double value
                        double latitude = double.Parse(newPoint[0]);
                        double longitude = double.Parse(newPoint[1]);

                        StringBuilder sb = new StringBuilder();

                        Fix fix = new Fix(point, latitude, longitude);

                        // Add to the final route list
                        finalRoute.Add(fix);
                    }
                    else // If it is a waypoint
                    {
                        JArray jsonFixes = JArray.Parse(JObject.Parse(fixesDataJson)["nat_fixes"].ToString());

                        try
                        {
                            JToken jsonFix = jsonFixes.Where(fix => fix["fix"].ToString() == point).First();

                            finalRoute.Add(new Fix(jsonFix["fix"].ToString(), (double) jsonFix["lat"], (double) jsonFix["lon"]));
                        }
                        catch (Exception)
                        {
                            // Add without coordinates
                            finalRoute.Add(new Fix(point));
                        }
                    }                    
                }

                // Build new track object
                Track trackObj = new Track
                {
                    Id = list[0][0],
                    TMI = tmi,
                    Route = finalRoute,
                    Direction = direction,
                    FlightLevels = flightLevels
                };

                returnList.Add(trackObj);
            }

            return returnList;
        }
    }
}
