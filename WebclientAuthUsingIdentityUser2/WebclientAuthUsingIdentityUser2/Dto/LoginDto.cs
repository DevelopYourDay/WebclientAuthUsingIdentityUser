﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace WebclientAuthUsingIdentityUser.Dto
{
    public class LoginDto
    {
        [Required]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} characters long.", MinimumLength = 3)]
        [Display(Name = "UserName")]
        public string UserName { get; set; }


        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }
}