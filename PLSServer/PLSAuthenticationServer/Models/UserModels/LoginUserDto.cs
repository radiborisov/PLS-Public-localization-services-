using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PLSAuthenticationServer.Models.UserModels
{
    public class LoginUserDto
    {
        [Required]
        [RegularExpression("^[0-9]+$")]
        public string PhoneNumber { get; set; }

        [Required]
        public string VerificationCode { get; set; }

        [Required]
        public string SecretKey { get; set; }
    }
}
