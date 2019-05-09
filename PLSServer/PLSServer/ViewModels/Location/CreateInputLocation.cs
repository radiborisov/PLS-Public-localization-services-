using System.ComponentModel.DataAnnotations;

namespace PLSServer.ViewModels.Location
{
    public class CreateInputLocation
    {
        [Required]
        public double Longitude { get; set; }

        [Required]
        public double Latitude { get; set; }

        [Required]
        public double Altitude { get; set; }

        [Required]
        public string PhoneNumber { get; set; }

        [Required]
        public string Token { get; set; }


    }
}
