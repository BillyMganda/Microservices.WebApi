using MediatR;
using System.ComponentModel.DataAnnotations;
using User.Microservice.DTOs;

namespace User.Microservice.CQRS
{
    public class LoginCommand : IRequest<AuthenticateResponse>
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string Password { get; set; } = string.Empty;
    }
}
