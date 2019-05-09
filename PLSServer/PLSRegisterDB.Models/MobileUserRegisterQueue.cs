using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PLSRegisterDB.Models
{
    public class MobileUserRegisterQueue
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("phonenumber")]
        public String PhoneNumber { get; set; }

        [Column("verificationcode")]
        public string VerificationCode { get; set; }

        [Column("secretkey")]
        public Guid SecretKey { get; } = Guid.NewGuid();

        [Column("isregistered")]
        public bool IsRegistered { get; set; } = false;

        [Column("registerisenabled")]
        public bool RegisterIsEnabled { get; set; } = true;

        [Column("enddate")]
        public DateTime EndDate { get; set; } = DateTime.Now.AddMinutes(5);
    }
}
