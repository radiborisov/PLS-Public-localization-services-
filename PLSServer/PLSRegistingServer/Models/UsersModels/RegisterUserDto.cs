﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace PLSRegistingServer.Models.UsersModels
{
    public class RegisterUserDto
    {
        [Required]
        [RegularExpression("^[0-9]+$")]
        public string PhoneNumber { get; set; }
    }
}
