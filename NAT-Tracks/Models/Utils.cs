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

        // List of months
        private static readonly string[] _months = { "JAN", "FEB", "MAR", "APR", "MAY", "JUN", "JUL", "AUG", "SEP", "OCT", "NOV", "DEC" };

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

            // So we can know which track corresponds to which date
            List<int> lineNumbers = new List<int>();
            
            // Dodgy but it works :)
            int getNextFour = 0;

            // Track
            List<string> track = new List<string>();

            List<string> validities = new List<string>();

            string validFrom = String.Empty;
            string validTo = String.Empty;

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
                            lineNumbers.Add(i);
                            continue;
                        }

                        // Add to the track
                        track.Add(splitList[i]);
                    }
                    else if (splitList[i].Contains("TMI IS")) 
                    {
                        // Get the tmi
                        tmi = Convert.ToString(splitList[i][splitList[i].IndexOf("TMI IS") + 7]) + Convert.ToString(splitList[i][splitList[i].IndexOf("TMI IS") + 8]) + Convert.ToString(splitList[i][splitList[i].IndexOf("TMI IS") + 9]);

                        // Add amendment character if it exists
                        if (char.IsLetter(splitList[i][splitList[i].IndexOf("TMI IS") + 10]))
                        {
                            tmi += Convert.ToString(splitList[i][splitList[i].IndexOf("TMI IS") + 10]);
                        }

                    } 
                    else
                    {
                        for (int j = 0; j < _months.Length; j++)
                        {
                            bool reached = false;
                            if (splitList[i].Contains(_months[j]))
                            {
                                // Get time
                                string[] splitString = splitList[i].Split('/');
                                validFrom = splitString[0][splitString[0].Length - 2].ToString() + splitString[0][splitString[0].Length - 1].ToString() 
                                    + "/" + splitString[1][0].ToString() + splitString[1][1].ToString() + splitString[1][2].ToString() + splitString[1][3].ToString();
                                validTo = splitString[0][splitString[0].Length - 2].ToString() + splitString[0][splitString[0].Length - 1].ToString()
                                    + "/" + splitString[2][0].ToString() + splitString[2][1].ToString() + splitString[2][2].ToString() + splitString[2][3].ToString();
                                // Parse the time
                                DateTime time = new DateTime(DateTime.UtcNow.Year, j + 1, Convert.ToInt32(validFrom.Split('/')[0]), Convert.ToInt32(validFrom.Split('/')[1].Substring(0, 2)), Convert.ToInt32(validFrom.Split('/')[1].Substring(2, 2)), 0);
                                validFrom = time.ToString("yyyy/MM/dd, HH:mm:ss");
                                time = new DateTime(DateTime.UtcNow.Year, j + 1, Convert.ToInt32(validTo.Split('/')[0]), Convert.ToInt32(validTo.Split('/')[1].Substring(0, 2)), Convert.ToInt32(validTo.Split('/')[1].Substring(2, 2)), 0);
                                validTo = time.ToString("yyyy/MM/dd, HH:mm:ss");
                                validities.Add(validFrom + "?" + validTo + "?" + i);
                                reached = true;
                            }
                            if (reached) // For performance
                            {
                                break;
                            }
                       
                        }
                    }
                }
                catch (Exception ex) // Catch any exception
                {
                    continue;
                }
            }

            // Final list to return
            List<Track> returnList = new List<Track>();

            // Build track objects
            int counter = 0;
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
                        double longitude = -(double.Parse(newPoint[1]));

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
                    FlightLevels = flightLevels,
                    ValidFrom = validities[Int32.Parse(validities[1].Split("?")[2]) > lineNumbers[counter] ? 0 : 1].Split("?")[0],
                    ValidTo = validities[Int32.Parse(validities[1].Split("?")[2]) > lineNumbers[counter] ? 0 : 1].Split("?")[1]
                };

                returnList.Add(trackObj);
                counter++;
            }

            return returnList;
        }
    }
}
