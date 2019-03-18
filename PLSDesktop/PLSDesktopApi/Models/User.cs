using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLSDesktopApi.Models
{
    class User
    {
        public User()
        {
            Locations = new List<Location>();
        }

        public int Id { get; set; }

        public string PhoneNumber { get; set; }

        public bool IsSavioer { get; set; } = false;

        public bool IsOnline { get; set; } = false;

        public List<Location> Locations { get; set; }
    }
}
