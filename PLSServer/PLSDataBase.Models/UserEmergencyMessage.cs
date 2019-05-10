using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace PLSDataBase.Models
{
    public class UserEmergencyMessage
    {
        [Key]
        [Column("id")]
        public int Id { get; set; }

        [Column("userid")]
        public int UserId { get; set; }
        public User User { get; set; }

        [Column("message")]
        public string Message { get; set; }

        [Column("created")]
        public DateTime Created { get; set; } = DateTime.Now;
    }
}
