using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PLSDataBase.Models
{
    public class RegistrationQueue
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("phonenumber")]
        public string PhoneNumber { get; set; }

        [Column("token")]
        public string VerificationCode { get; set; }

        [Column("isregistered")]
        public bool IsRegistered { get; set; } = false;
    }
}
