using AutoMapper;
using GMap.NET;
using GMap.NET.MapProviders;
using GMap.NET.WindowsForms;
using GMap.NET.WindowsForms.Markers;
using Newtonsoft.Json;
using PLSDesktopApi.MappingConfiguration;
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
        List<UserDto> users = new List<UserDto>();
        List<GMapMarker> gmapMarkers = new List<GMapMarker>();

        public Form1()
        {
            InitializeComponent();

            Mapper.Initialize(config =>
            {
                config.AddProfile<PLSDesktopProfile>();
            });
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
                    int lastLocationIndex = user.Locations.Count - 1;

                    userBox.Items.Add(user.PhoneNumber);

                    if (lastLocationIndex >= 0)
                    {


                        AddMarkers(user, user.Locations[lastLocationIndex]);
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
            List<UserDto> createInputUsers = new List<UserDto>();

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

            var userInput = JsonConvert.DeserializeObject<List<UserDto>>(result);

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

            if (currentPhoneNumber.ToLower() == "All users".ToLower() || currentPhoneNumber == "")
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
                    for (int i = 0; i < user.Locations.Count; i++)
                    {
                        if (i > 0)
                        {
                            var lastUserLocation = user.Locations[i - 1];
                            var currentUserLocation = user.Locations[i];

                            if (lastUserLocation.Latitude == currentUserLocation.Latitude && lastUserLocation.Longitude == currentUserLocation.Longitude)
                            {
                                continue;
                            }
                        }

                        AddMarkers(user, user.Locations[i]);
                    }

                    for (int i = 0; i < user.Locations.Count - 1; i++)
                    {
                        if (i == user.Locations.Count - 1)
                        {
                            break;
                        }



                        if (i > 0)
                        {
                            var lastUserLocation = user.Locations[i - 1];
                            var currentUserLocation = user.Locations[i];

                            if (lastUserLocation.Latitude == currentUserLocation.Latitude && lastUserLocation.Longitude == currentUserLocation.Longitude)
                            {
                                continue;
                            }
                        }

                        AddPolygonesLines(user, user.Locations[i], user.Locations[i + 1]);
                    }


                    VisualiseMarkers(markers);
                }
            }
        }

        private void AddMarkers(UserDto currentUser, LocationDto location)
        {
            StringBuilder sb = new StringBuilder();
            GMapMarker marker = new GMarkerGoogle(
                            new PointLatLng(location.Latitude, location.Longitude),
                            currentUser.Marker);
            sb.AppendLine(currentUser.PhoneNumber);
            sb.AppendLine($"Longitude: {location.Longitude} , Latitude: {location.Latitude} , Altitude: {location.Altitude} | Date {location.Date.ToString("dd-MM-yyyy HH:mm:ss")}");
            marker.ToolTipText = sb.ToString().TrimEnd();
            gmapMarkers.Add(marker);
        }

        private void AddPolygonesLines(UserDto currentUser, LocationDto location1, LocationDto location2)
        {
            GMapOverlay polyOverlay = new GMapOverlay("polygons");
            List<PointLatLng> points = new List<PointLatLng>();
            points.Add(new PointLatLng(location1.Latitude, location1.Longitude));
            points.Add(new PointLatLng(location2.Latitude, location2.Longitude));
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



        private void SendPutRequestIsSavior(string phoneNumber, bool isSavior)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://public-localization-services-desktop.azurewebsites.net/return/user");
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var user = users.FirstOrDefault(u => u.PhoneNumber == phoneNumber);

                var userDto = Mapper.Map<ChangeUsersRank>(user);
                userDto.IsSavior = isSavior;

                var jsonUser = JsonConvert.SerializeObject(userDto);

                streamWriter.Write(jsonUser);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                //Now you have your response.
                //or false depending on information in the response

            }
        }


        private void SendPutRequestIsInDanger(string phoneNumber, bool isInDanger)
        {
            var httpWebRequest = (HttpWebRequest)WebRequest.Create("https://public-localization-services-desktop.azurewebsites.net/return/user/" + phoneNumber);
            httpWebRequest.ContentType = "application/json";
            httpWebRequest.Method = "PUT";

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                var userCondition = new ChangeUserCondition();
                userCondition.IsInDanger = isInDanger;

                var jsonUser = JsonConvert.SerializeObject(userCondition);

                streamWriter.Write(jsonUser);
            }
            var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
            using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
            {
                var responseText = streamReader.ReadToEnd();
                //Now you have your response.
                //or false depending on information in the response

            }
        }

        private void contextMenuStrip1_Opening(object sender, System.ComponentModel.CancelEventArgs e)
        {

        }

        private void changeToSaviorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (userBox.SelectedIndex == 1)
            {
                return;
            }

            string phoneNumber = userBox.GetItemText(userBox.SelectedItem);

            SendPutRequestIsSavior(phoneNumber, true);

            GetInformation();

            AddingMarkers();
        }

        private void changeToTouristToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (userBox.SelectedIndex == -1)
            {
                return;
            }

            string phoneNumber = userBox.GetItemText(userBox.SelectedItem);

            SendPutRequestIsSavior(phoneNumber, false);

            GetInformation();

            AddingMarkers();
        }

        private void changeUserConditionToSavedToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (userBox.SelectedIndex == -1)
            {
                return;
            }

            string phoneNumber = userBox.GetItemText(userBox.SelectedItem);

            SendPutRequestIsInDanger(phoneNumber, false);

            GetInformation();

            AddingMarkers();
        }

        private void changeUserConditionToEmergencyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (userBox.SelectedIndex == -1)
            {
                return;
            }

            string phoneNumber = userBox.GetItemText(userBox.SelectedItem);

            SendPutRequestIsInDanger(phoneNumber, true);

            GetInformation();

            AddingMarkers();
        }
    }
}
