using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PLSServer.ViewModels.User
{
    public class ChangeUserCondition
    {
        [Required]
        [RegularExpression("^[0-9]+$")]
        public string PhoneNumber { get; set; }

        [Required]
        public bool IsInDanger { get; set; }

        [Required]
        public string Token { get; set; }
    }
}
