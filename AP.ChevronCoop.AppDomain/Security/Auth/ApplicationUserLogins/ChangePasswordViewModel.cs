﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AP.ChevronCoop.AppDomain.Security.Auth.ApplicationUserLogins
{
    public class ChangePasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [StringLength(512, MinimumLength = 10)]
        public string OldPassword { get; set; }

        [Required]
        [StringLength(512, MinimumLength = 10)]
        public string NewPassword { get; set; }

        [Required]
        [StringLength(50, MinimumLength = 10)]
        public string ConfirmPassword { get; set; }
    }
}
