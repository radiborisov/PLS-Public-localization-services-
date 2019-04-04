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
         
            GMapOverlay markers = new GMapOverlay("markers");

            VisualiseMarkers(markers);
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {
            gMapControl1.DragButton = MouseButtons.Left;
            gMapControl1.MapProvider = GMapProviders.BingHybridMap;
            gMapControl1.Position = new PointLatLng(42.666551, 23.350466);
            gMapControl1.MinZoom = 5;
            gMapControl1.MaxZoom = 100;
            gMapControl1.Zoom = 10;
        }

        private void listBox1_SelectedIndexChanged_1(object sender, EventArgs e)
        {
            gMapControl1.Overlays.Clear();
            gmapMarkers.Clear();

            GMapOverlay markers = new GMapOverlay("markers");

            var currentPhoneNumber = listBox1.GetItemText(listBox1.SelectedItem);

            if (currentPhoneNumber.ToLower() == "All users".ToLower())
            {
                foreach (var currentUser in users)
                {
                    if (currentUser.Locations.Count - 1 >= 0)
                    {
                        var location = currentUser.Locations[currentUser.Locations.Count - 1];
                        AddMarkers(currentUser, location);
                    }
                }
                
                VisualiseMarkers(markers);
            }
            else
            {
                var user = users.FirstOrDefault(p => p.PhoneNumber == currentPhoneNumber);

                foreach (var location in user.Locations)
                {
                    AddMarkers(user, location);
                }

                VisualiseMarkers(markers);
            }

            GetInformation();
        }

        private void VisualiseMarkers(GMapOverlay markers)
        {
            foreach (var item in gmapMarkers)
            {
                gMapControl1.Overlays.Add(markers);
                markers.Markers.Add(item);
                gMapControl1.ShowCenter = true;
            }
        }

        private void AddMarkers(CreateInputUser currentUser, LocationDto location)
        {
            StringBuilder sb = new StringBuilder();
            GMapMarker marker = new GMarkerGoogle(
                            new PointLatLng(location.Longitude, location.Latitude),
                            currentUser.Marker);
            sb.AppendLine(currentUser.PhoneNumber);
            sb.AppendLine($"Longitude: {location.Longitude} , Latitude: {location.Latitude} , Altitude: {location.Altitude} | Date {location.Date.ToString("dd-MM-yyyy HH:mm:ss")}");
            marker.ToolTipText = sb.ToString().TrimEnd();
            gmapMarkers.Add(marker);
        }

        private void GetInformation()
        {
            int oldUsersCount = users.Count();

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

            users.Clear(); 

            var userInput = JsonConvert.DeserializeObject<List<CreateInputUser>>(result);

            foreach (var user in userInput)
            {
                users.Add(user);
            }

            if (oldUsersCount != userInput.Count)
            {
                UpdateListBox();
            }
        }

        private void UpdateListBox()
        {
            listBox1.Items.Clear();

            listBox1.Items.Add("All users");

            foreach (var user in users)
            {
                listBox1.Items.Add(user.PhoneNumber);
                foreach (var location in user.Locations)
                {
                    AddMarkers(user, location);
                }
            }
        }
    }
}
