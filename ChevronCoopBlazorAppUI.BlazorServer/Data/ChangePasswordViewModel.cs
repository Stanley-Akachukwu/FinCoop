﻿namespace ChevronCoop.Web.AppUI.BlazorServer.Data
{
    public class ChangePasswordViewModel
    {

        public string Email { get; set; }

        public string OldPassword { get; set; }

        public string NewPassword { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
