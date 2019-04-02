using System;

namespace PLSServer.ViewModels.Location
{
    public class CreateOutputLocation
    {
        public double Latitude { get; set; }

        public double Longitude { get; set; }

        public double Altitude { get; set; }

        public DateTime Date { get; set; }

        public int UserId { get; set; }
    }
}
