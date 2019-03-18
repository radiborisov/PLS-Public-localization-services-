using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GMap.NET;
using GMap.NET.MapProviders;
using System.Net;
using System.IO;
using System.Windows.Forms;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using PLSDesktopApi.Models;

namespace PLSDesktopApi
{
    public partial class Form1 : Form
    {
        Dictionary<string, List<Location>> usersLocations = new Dictionary<string, List<Location>>();
        List<GMapMarker> gmapMarkers = new List<GMapMarker>();
        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {

            GetInformation();

            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MapProvider = GMapProviders.BingHybridMap;
            gMapControl1.Position = new PointLatLng(12, 16);
            gMapControl1.MinZoom = 5;
            gMapControl1.MaxZoom = 100;
            gMapControl1.Zoom = 10;

            GMapOverlay markers = new GMapOverlay("markers");

            foreach (var userKey in usersLocations)
            {
                for (int i = 0; i < userKey.Value.Count(); i += 3)
                {
                    GMapMarker marker = new GMarkerGoogle(
                                    new PointLatLng(userKey.Value[i].Longitude, userKey.Value[i].Latitude),
                                    GMarkerGoogleType.blue_pushpin);
                    gmapMarkers.Add(marker);
                }
            }

            foreach (var item in gmapMarkers)
            {
                markers.Markers.Add(item);
                gMapControl1.Overlays.Add(markers);
                gMapControl1.ShowCenter = false;
            }
        }

        private void GetInformation()
        {
            StringBuilder sb = new StringBuilder();



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

            var list = result.Split(new char[] { ' ', '"', '{', '}' }, StringSplitOptions.RemoveEmptyEntries).ToList();

            Console.WriteLine(result);


            for (int i = 0; i < list.Count; i += 2)
            {
                var list2 = list[i + 1].Split(new char[] { ',', '[', ']', ':' }, StringSplitOptions.RemoveEmptyEntries).ToList();
                if (!usersLocations.ContainsKey(list[i]))
                {
                    usersLocations.Add(list[i], new List<Location>());
                }
                else
                {

                    for (int j = 0; j < list2.Count; j += 3)
                    {
                        Location location = new Location();

                        location.Longitude = double.Parse(list2[j]);
                        location.Latitude = double.Parse(list2[j + 1]);
                        location.Altitude = double.Parse(list2[j + 2]);

                        usersLocations[list[i]].Add(location);
                    }
                    return;
                }

                for (int j = 0; j < list2.Count; j += 3)
                {
                    Location location = new Location();

                    location.Longitude = double.Parse(list2[j]);
                    location.Latitude = double.Parse(list2[j + 1]);
                    location.Altitude = double.Parse(list2[j + 2]);

                    usersLocations[list[i]].Add(location);
                }
            }
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
