using PLSDataBase.Models;
using PLSServerForDesktop.ViewModels.Locations;
using System.Collections.Generic;

namespace PLSServerForDesktop.ViewModels.Users
{
    public class CreateUserAllView
    {
        public string PhoneNumber { get; set; }

        public bool IsSavioer { get; set; }

        public bool IsOnline { get; set; }

        public List<CreateLocationAllView> Locations { get; set; }
    }
}
