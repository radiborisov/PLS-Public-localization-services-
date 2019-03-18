using PLSDataBase.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace PLSDataBase.Initilizer.Generators
{
    public class LocationGenerator
    {
        internal static Location[] CreateLocations()
        {
            string[] inputLocations = new string[]
            {
                "Longitude=42.679698, Latitude=23.345243, Altitude=2, UserId=1",
                "Longitude=43.679698, Latitude=24.345243, Altitude=5, UserId=2",
                "Longitude=41.679698, Latitude=22.345243, Altitude=10, UserId=3"
            };

            Location[] users = new Location[inputLocations.Length];

            for (int i = 0; i < users.Length; i++)
            {
                string[] splitedInput = inputLocations[i].Split(new char[] { ' ', ',', '=' }, StringSplitOptions.RemoveEmptyEntries);

                decimal longitude = decimal.Parse(splitedInput[1]);
                decimal latitude = decimal.Parse(splitedInput[3]);
                decimal altitude = decimal.Parse(splitedInput[5]);
                int userId = int.Parse(splitedInput[7]);

                users[i].Longitude = longitude;
                users[i].Latitude = latitude;
                users[i].Altitude = altitude;
                users[i].UserId = userId;
            }

            return users;
        }
    }
}
