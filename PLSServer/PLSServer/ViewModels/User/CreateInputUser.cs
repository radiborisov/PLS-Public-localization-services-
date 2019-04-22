using System.ComponentModel.DataAnnotations;

namespace PLSMobileServer.ViewModels.User
{
    public class CreateInputUser
    {
        [Required]
        [RegularExpression("^[0-9]+$")]
        public string PhoneNumber { get; set; }
    }
}
