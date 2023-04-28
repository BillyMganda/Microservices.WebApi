using MediatR;
using System.ComponentModel.DataAnnotations;

namespace User.Microservice.CQRS
{
    public class ForgotPasswordCommand : IRequest
    {
        [Required, EmailAddress]
        public string Email { get; set; } = string.Empty;
    }
}
