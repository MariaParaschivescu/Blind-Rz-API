using System.ComponentModel.DataAnnotations;


namespace Application.DTOs
{
    public class RegisterUserDTO
    {
        [Required(AllowEmptyStrings = false, ErrorMessage = "Email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        public string Email { get; set; }

        [Required(AllowEmptyStrings = false, ErrorMessage = "Password is required")]
        [MinLength(8)]
        public string Password { get; set; }

        [Required]
        [Compare("Password")]
        public string ConfirmPassword { get; set; }

        [Required]
        public string UserName { get; set; }

        public string? Phone { get; set; }
        public string Role { get; set; } = "User";
        public string ResetToken { get; set; } = "";
        public string VerificationToken { get; set; } = "";
    }
}
