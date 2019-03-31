using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Newtonsoft.Json;
using PLSDesktopApi.Models.Location;
using PLSDesktopApi.Models.User;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Windows.Forms;

namespace PLSDesktopApi
{
    public partial class Form1 : Form
    {
        List<CreateInputUser> users = new List<CreateInputUser>();
        List<GMapMarker> gmapMarkers = new List<GMapMarker>();

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {


            GetInformation();

            foreach (var user in users)
            {
                listBox1.Items.Add(user.PhoneNumber);
                foreach (var location in user.Locations)
                {
                    StringBuilder sb = new StringBuilder();
                    GMapMarker marker = new GMarkerGoogle(
                                    new PointLatLng(location.Longitude, location.Latitude),
                                    GMarkerGoogleType.blue_pushpin);
                    sb.AppendLine(user.PhoneNumber);
                    sb.AppendLine($"Longitude {location.Longitude} , Latitude {location.Latitude} , Altitude {location.Altitude}.");
                    marker.ToolTipText = sb.ToString().TrimEnd();
                    gmapMarkers.Add(marker);
                }
            }

            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MapProvider = GMapProviders.BingHybridMap;
            gMapControl1.Position = new PointLatLng(42.666551, 23.350466);
            gMapControl1.MinZoom = 5;
            gMapControl1.MaxZoom = 100;
            gMapControl1.Zoom = 10;

            GMapOverlay markers = new GMapOverlay("markers");

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
            string url = @"http://public-localization-services-desktop-server.azurewebsites.net/return/user";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            var userInput = JsonConvert.DeserializeObject<List<CreateInputUser>>(result);

            foreach (var user in userInput)
            {
                users.Add(user);
            }
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {

        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {

            gMapControl1.Overlays.Clear();
            var currentPhoneNumber = listBox1.GetItemText(listBox1.SelectedItem);
            gmapMarkers.Clear();

            //foreach (var location in user.Locations)
            //{
            //    StringBuilder sb = new StringBuilder();

            //    GMapMarker marker = new GMarkerGoogle(
            //                    new PointLatLng(location.Longitude, location.Latitude),
            //                    GMarkerGoogleType.blue_pushpin);

            //    sb.AppendLine(user.PhoneNumber);
            //    sb.AppendLine($"Longitude {location.Longitude} , Latitude {location.Latitude} , Altitude {location.Altitude}.");

            //    marker.ToolTipText = sb.ToString().TrimEnd();
            //    gmapMarkers.Add(marker);

            //}
            if (currentPhoneNumber.ToLower() == "All users".ToLower())
            {
                foreach (var currentUser in users)
                {
                    StringBuilder sb = new StringBuilder();
                    var location = currentUser.Locations[currentUser.Locations.Count - 1];
                    GMapMarker marker = new GMarkerGoogle(
                                    new PointLatLng(location.Longitude, location.Latitude),
                                    GMarkerGoogleType.blue_pushpin);
                    sb.AppendLine(currentUser.PhoneNumber);
                    sb.AppendLine($"Longitude {location.Longitude} , Latitude {location.Latitude} , Altitude {location.Altitude}.");
                    marker.ToolTipText = sb.ToString().TrimEnd();
                    gmapMarkers.Add(marker);
                   
                }

                GMapOverlay markers = new GMapOverlay("markers");

                foreach (var item in gmapMarkers)
                {
                    markers.Markers.Add(item);
                    gMapControl1.Overlays.Add(markers);
                    gMapControl1.ShowCenter = false;
                }


            }
            else
            {
                var user = users.FirstOrDefault(p => p.PhoneNumber == currentPhoneNumber);

                foreach (var location in user.Locations)
                {
                    StringBuilder sb = new StringBuilder();
                    GMapMarker marker = new GMarkerGoogle(
                                    new PointLatLng(location.Longitude, location.Latitude),
                                    GMarkerGoogleType.blue_pushpin);
                    sb.AppendLine(user.PhoneNumber);
                    sb.AppendLine($"Longitude {location.Longitude} , Latitude {location.Latitude} , Altitude {location.Altitude}.");
                    marker.ToolTipText = sb.ToString().TrimEnd();
                    gmapMarkers.Add(marker);
                }



                GMapOverlay markers = new GMapOverlay("markers");

                foreach (var item in gmapMarkers)
                {
                    markers.Markers.Add(item);
                    gMapControl1.Overlays.Add(markers);
                    gMapControl1.ShowCenter = false;
                }
            }



        }
    }
}
