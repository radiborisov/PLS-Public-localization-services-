using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace EFCodeFirst.Infrastructure.Data.Models
{
    public class User
    {
        public User()
        {
            Locations = new HashSet<Location>();
        }

        [Key]
        public int Id { get; set; }

        [StringLength(10, MinimumLength = 10)]
        public string PhoneNumber { get; set; }

        public bool IsOnline { get; set; } = false;

        public HashSet<Location> Locations { get; set; }
    }
}
