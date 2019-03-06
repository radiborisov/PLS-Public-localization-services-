using PLSServer.DBContext.Data;
using PLSServer.DBContext.Data.Models;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SandProject
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            IDictionary<string, List<decimal>> usersLocations = new Dictionary<string, List<decimal>>();

            using (PLSDBContext context = new PLSDBContext())
            {
                var userLocations = context.Users.Select(u => new
                {
                    u.PhoneNumber,
                    locations = u.Locations
                });

                foreach (var item in userLocations)
                {
                    usersLocations.Add(item.PhoneNumber, new List<decimal>());

                    foreach (var location in item.locations)
                    {
                        usersLocations[item.PhoneNumber].Add(location.Longitude);
                        usersLocations[item.PhoneNumber].Add(location.Latitude);
                        usersLocations[item.PhoneNumber].Add(location.Altitude);
                    }
                }
            }

            foreach (var item in usersLocations)
            {
                var location = item.Value;
                    sb.Append($"{item.Key} ");
                for (int i = 0; i < item.Value.Count; i+=3)
                {
                    sb.Append($"{item.Value[i]}:{item.Value[i + 1]}:{item.Value[i + 2]}/");
                }
                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString().TrimEnd());
        }

    }
}
