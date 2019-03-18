using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PLSDataBase.Models
{
    public class User
    {
        public User()
        {
            Locations = new List<Location>();
        }

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [StringLength(10, MinimumLength = 10)]
        [Column("phonenumber")]
        public string PhoneNumber { get; set; }

        [Column("issavioer")]
        public bool IsSavioer { get; set; } = false;
 
        [Column("isonline")]
        public bool IsOnline { get; set; } = false;

        public List<Location> Locations { get; set; }
    }
}
