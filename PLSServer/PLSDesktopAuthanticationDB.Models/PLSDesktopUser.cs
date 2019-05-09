using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PLSDesktopAuthanticationDB.Models
{
    public class PLSDesktopUser
    {
        [Column("accessfailedcount")]
        public int AccessFailedCount { get; set; } = 0;

        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("username")]
        public string Username { get; set; }

        [Column("password")]
        public string Password { get; set; }

        [Column("secretkey")]
        public Guid SecretKey { get; set; } = Guid.NewGuid();

        [Column("lockoutenabled")]
        public bool LockoutEnabled { get; set; } = false;

        [Column("lockoutend")]
        public DateTime LockoutEnd { get; set; }

        [Column("enddate")]
        public DateTime EndDate { get; set; } = DateTime.Now.AddMinutes(5);
    }
}
