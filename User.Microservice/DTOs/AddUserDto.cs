using System.ComponentModel.DataAnnotations;

namespace User.Microservice.DTOs
{
    public class AddUserDto
    {
        [Required]
        public string FirstName { get; set; } = string.Empty;
        [Required]
        public string LastName { get; set; } = string.Empty;
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
        [Required, MinLength(6), Compare("Password")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
