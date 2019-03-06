using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace PLSServer.DBContext.Data.Models
{
    public class Location
    {
        [Key]      
        [Column("id")]
        public int Id { get; set; }
    
        [Required]
        [Column("latitude")]
        public decimal Latitude { get; set; }

        [Required]
        [Column("longitude")]
        public decimal Longitude { get; set; }

        [Required]
        [Column("altitude")]
        public decimal Altitude { get; set; }

        [Required]
        [Column("userid")]
        public int UserId { get; set; }
        public User User { get; set; }
    }
}
