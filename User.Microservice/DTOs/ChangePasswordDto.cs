using System.ComponentModel.DataAnnotations;

namespace User.Microservice.DTOs
{
    public class ChangePasswordDto
    {
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string NewPassword { get; set; } = string.Empty;
        [Required, MinLength(6), Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
