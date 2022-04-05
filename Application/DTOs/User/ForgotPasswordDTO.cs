﻿using System.ComponentModel.DataAnnotations;

namespace Application.DTOs
{
    public class ForgotPasswordDTO
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
