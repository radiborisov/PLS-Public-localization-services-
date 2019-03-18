using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;

namespace SandProject
{
    class Program
    {
        static void Main(string[] args)
        {
            StringBuilder sb = new StringBuilder();
            IDictionary<string, List<decimal>> usersLocations = new Dictionary<string, List<decimal>>();


            string result = string.Empty;
            string url = @"https://localhost:44301/api/values";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            var list = result.Split(new char[]{' ','"','{','}'},StringSplitOptions.RemoveEmptyEntries).ToList();

            Console.WriteLine(result);


            for (int i = 0; i < list.Count; i+=2)
            {
                usersLocations.Add(list[i], new List<decimal>());
                var list2 = list[i + 1].Split(new char[] { ',', '[', ']',':'},StringSplitOptions.RemoveEmptyEntries).ToList();
                for (int j = 0; j < list2.Count; j+=3)
                {
                    usersLocations[list[i]].Add(decimal.Parse(list2[j]));
                }
            }

            foreach (var item in usersLocations)
            {
                sb.Append(item.Key + " ");

                foreach (var location in item.Value)
                {
                    sb.Append(location + " ");
                }
                sb.AppendLine();
            }

            Console.WriteLine(sb.ToString().TrimEnd());



            //using (PLSDBContext context = new PLSDBContext())
            //{
            //    var userLocations = context.Users.Select(u => new
            //    {
            //        u.PhoneNumber,
            //        locations = u.Locations
            //    });

            //    foreach (var item in userLocations)
            //    {
            //        usersLocations.Add(item.PhoneNumber, new List<decimal>());

            //        foreach (var location in item.locations)
            //        {
            //            usersLocations[item.PhoneNumber].Add(location.Longitude);
            //            usersLocations[item.PhoneNumber].Add(location.Latitude);
            //            usersLocations[item.PhoneNumber].Add(location.Altitude);
            //        }
            //    }
            //}

            //foreach (var item in usersLocations)
            //{
            //    var location = item.Value;
            //        sb.Append($"{item.Key} ");
            //    for (int i = 0; i < item.Value.Count; i+=3)
            //    {
            //        sb.Append($"{item.Value[i]}:{item.Value[i + 1]}:{item.Value[i + 2]}/");
            //    }
            //    sb.AppendLine();
            //}

            //Console.WriteLine(sb.ToString().TrimEnd());
        }

    }
}
