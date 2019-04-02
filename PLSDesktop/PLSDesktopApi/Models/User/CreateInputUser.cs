﻿using GMap.NET.WindowsForms.Markers;
using Newtonsoft.Json.Linq;
using PLSDesktopApi.Models.Location;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLSDesktopApi.Models.User
{
    public class CreateInputUser
    {
        public string PhoneNumber { get; set; }

        public bool IsSavioer { get; set; }

        public bool IsOnline { get; set; }

        public GMarkerGoogleType Marker { get => SetMarker();}

        public List<LocationDto> Locations { get; set; }

        private GMarkerGoogleType SetMarker()
        {
            if (IsSavioer)
            {
                return GMarkerGoogleType.red_small;
            }
            else
            {
                return GMarkerGoogleType.blue_small;
            }
        }
    }
}
