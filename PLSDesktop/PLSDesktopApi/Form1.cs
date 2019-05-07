using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Newtonsoft.Json;
using PLSDesktopApi.Models.Location;
using PLSDesktopApi.Models.User;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
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
            SendRequest();

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
            AddingMarkers();
        }

        private void UpdateListBox(string phoneNumbers, bool searchForPhoneNumbers)
        {
            userBox.Items.Clear();

            userBox.Items.Add("All users");

            if (!searchForPhoneNumbers)
            {
                foreach (var user in users)
                {
                    userBox.Items.Add(user.PhoneNumber);
                    foreach (var location in user.Locations)
                    {
                        AddMarkers(user, location);
                    }
                }
            }
            else
            {
                foreach (var user in users)
                {
                    if (user.PhoneNumber.Contains(phoneNumbers))
                    {
                        userBox.Items.Add(user.PhoneNumber);
                        foreach (var location in user.Locations)
                        {
                            AddMarkers(user, location);
                        }
                    }

                }
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            TextBox textBox = (TextBox)sender;

            string textValue = textBox.Text;

            UpdateListBox(textValue, true);
        }

        private void panel1_Paint(object sender, PaintEventArgs e)
        {


        }

        private void GetInformation()
        {
            List<CreateInputUser> createInputUsers = new List<CreateInputUser>();

            int oldUsersCount = users.Count();

            StringBuilder sb = new StringBuilder();

            string result = string.Empty;
            string url = @"http://public-localization-services-desktop.azurewebsites.net/return/user";

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
                createInputUsers.Add(user);
            }

            users.Clear();
            users = createInputUsers;

            if (oldUsersCount != userInput.Count)
            {
                UpdateListBox("", false);
            }
        }

        private async void SendRequest()
        {
            while (true)
            {
                GetInformation();

                if (userBox.SelectedIndex != -1)
                {
                    AddingMarkers();
                }


                await Task.Delay(60000);
            }
        }




        public void AddingMarkers()
        {
            gMapControl1.Overlays.Clear();
            gMapControl1.Refresh();
            gmapMarkers.Clear();

            GMapOverlay markers = new GMapOverlay("markers");

            var currentPhoneNumber = userBox.GetItemText(userBox.SelectedItem);

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

                if (user.Locations.Count > 0)
                {
                    foreach (var location in user.Locations)
                    {
                        AddMarkers(user, location);
                    }

                    for (int i = 0; i < user.Locations.Count - 1; i++)
                    {
                        if (i == user.Locations.Count - 1)
                        {
                            break;
                        }

                        AddPolygonesLines(user, user.Locations[i], user.Locations[i+1]);
                    }


                    VisualiseMarkers(markers);
                }
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

        private void AddPolygonesLines(CreateInputUser currentUser, LocationDto location1, LocationDto location2)
        {
            GMapOverlay polyOverlay = new GMapOverlay("polygons");
            List<PointLatLng> points = new List<PointLatLng>();          
            points.Add(new PointLatLng(location1.Longitude, location1.Latitude));       
            points.Add(new PointLatLng(location2.Longitude, location2.Latitude));       
            GMapPolygon polygon = new GMapPolygon(points, "mypolygon");
            polygon.Fill = new SolidBrush(Color.FromArgb(50, Color.Red));
            polygon.Stroke = new Pen(Color.Red, 1);
            gMapControl1.Overlays.Add(polyOverlay);
            polyOverlay.Polygons.Add(polygon);

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

    }
}
