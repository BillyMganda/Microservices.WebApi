using MediatR;
using System.ComponentModel.DataAnnotations;
using User.Microservice.DTOs;

namespace User.Microservice.CQRS
{
    public class ForgotPasswordCommand : IRequest<GetUserDto>
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
