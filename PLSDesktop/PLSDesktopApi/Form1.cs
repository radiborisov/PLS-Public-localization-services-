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
using Newtonsoft.Json;

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

            foreach (var user in users)
            {
                for (int i = 0; i < user.Locations.Count(); i += 3)
                {
                    GMapMarker marker = new GMarkerGoogle(
                                    new PointLatLng(user.Locations[i].Longitude, user.Locations[i].Latitude),
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
            string url = @"https://localhost:44301/return/user";

            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
            request.AutomaticDecompression = DecompressionMethods.GZip;

            using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
            using (Stream stream = response.GetResponseStream())
            using (StreamReader reader = new StreamReader(stream))
            {
                result = reader.ReadToEnd();
            }

            var user = JsonConvert.DeserializeObject<CreateInputUser>(result);

            users.Add(user);
        }

        private void gMapControl1_Load(object sender, EventArgs e)
        {

        }
    }
}
