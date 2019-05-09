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

        [Column("accessfailedcount")]
        public int AccessFailedCount { get; set; }

        [StringLength(10, MinimumLength = 10)]
        [Column("phonenumber")]
        public string PhoneNumber { get; set; }

        [Column("issavior")]
        public bool IsSavior { get; set; } = false;
 
        [Column("isonline")]
        public bool IsOnline { get; set; } = false;

        [Column("isindanger")]
        public bool IsInDanger { get; set; } = false;

        [Column("lockoutenabled")]
        public bool LockoutEnabled { get; set; } = false;

        [Column("lockoutend")]
        public DateTime LockoutEnd { get; set; }

        [Column("token")]
        public Guid Token { get; set; } = Guid.NewGuid();

        [Column("secret")]
        public Guid Secret { get; set; } = Guid.NewGuid();

        public List<Location> Locations { get; set; }
    }
}
