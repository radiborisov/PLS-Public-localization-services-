using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PLSServer.Data
{
    public class Location
    {
        public int Id { get; set; }

        public decimal Longitude { get; set; }

        public decimal Latitude { get; set; }

        public int UserId { get; set; }
    }
}
