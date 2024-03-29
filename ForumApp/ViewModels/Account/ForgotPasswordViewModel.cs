﻿using System.ComponentModel.DataAnnotations;

namespace ForumApp.ViewModels.Account
{
    public class ForgotPasswordViewModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
