using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PLSDesktopApi.Models
{
    public class CreateInputUser
    {
        public string PhoneNumber { get; set; }

        public bool IsSavioer { get; set; }

        public bool IsOnline { get; set; }

        public List<CreateLocation> Locations { get; set; }
    }
}
