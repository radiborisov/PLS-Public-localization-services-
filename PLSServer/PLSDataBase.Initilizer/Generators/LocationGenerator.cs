using PLSDataBase.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace PLSDataBase.Initilizer.Generators
{
    public class LocationGenerator
    {
        internal static Location[] CreateLocations()
        {
            string[] inputLocations = new string[]
            {
                "42.679698 23.345243 2 1",
                "43.679698 24.345243 5 2",
                "41.679698 22.345243 10 3"
            };

            Location[] users = new Location[inputLocations.Length];

            for (int i = 0; i < users.Length; i++)
            {
                string[] splitedInput = inputLocations[i].Split().ToArray();

                decimal longitude = decimal.Parse(splitedInput[0]);
                decimal latitude = decimal.Parse(splitedInput[1]);
                decimal altitude = decimal.Parse(splitedInput[2]);
                int userId = int.Parse(splitedInput[3]);

                Location newLocation = new Location()
                {
                    Longitude = longitude,
                    Latitude = latitude,
                    Altitude = altitude,
                    UserId = userId
                };

                users[i] = newLocation;
            }

            return users;
        }
    }
}
