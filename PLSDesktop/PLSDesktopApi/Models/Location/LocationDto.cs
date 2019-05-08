using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLSDesktopApi.Models.Location
{
    public class LocationDto
    {
        public double Longitude { get; set; }

        public double Latitude { get; set; }

        public double Altitude { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }
    }
}
