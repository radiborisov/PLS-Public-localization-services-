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
        public String PhoneNumber { get; set; }

        [Column("verificationcode")]
        public string VerificationCode { get; set; }

        [Column("isregistered")]
        public bool IsRegistered { get; set; } = false;

        [Column("startdate")]
        public DateTime StartDate { get; set; } = DateTime.Now;

        [Column("enddate")]
        public DateTime EndDate { get; set; } = DateTime.Now.AddMinutes(5);
    }
}
