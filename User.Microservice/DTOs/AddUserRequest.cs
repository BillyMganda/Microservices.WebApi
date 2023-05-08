using System.ComponentModel.DataAnnotations;

namespace User.Microservice.DTOs
{
    public class AddUserRequest
    {
        [Required]
        public string FirstName { get; set; }
        [Required]
        public string LastName { get; set; }
        [Required, EmailAddress]
        public string Username { get; set; }
        [Required, MinLength(6)]
        public string Password { get; set; }
    }
}
