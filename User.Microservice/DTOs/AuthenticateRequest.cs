using System.ComponentModel.DataAnnotations;

namespace User.Microservice.DTOs
{
    public class AuthenticateRequest
    {
        [Required, EmailAddress]
        public string Username { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
