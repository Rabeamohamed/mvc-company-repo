﻿using System.ComponentModel.DataAnnotations;

namespace Demo.Pl.VeiwModels
{
    public class ResetPasswordViewModel
    {
        [Required(ErrorMessage ="New Password is Required")]
        [DataType(DataType.Password)]

        public string NewPassword { get; set; }

        [Required(ErrorMessage = "Confirm New Password is Required")]
        [DataType(DataType.Password)]
        [Compare("NewPassword", ErrorMessage = "Password Doesn't Match")]

        public string ConfirmedNewPassword { get; set; }
    }
}
