using MediatR;
using System.ComponentModel.DataAnnotations;
using User.Microservice.DTOs;

namespace User.Microservice.CQRS
{
    public class ChangePasswordCommand : IRequest<GetUserDto>
    {
        [Required]
        public string Token { get; set; } = string.Empty;
        [Required, MinLength(6)]
        public string NewPassword { get; set; } = string.Empty;
        [Required, MinLength(6), Compare("NewPassword")]
        public string ConfirmNewPassword { get; set; } = string.Empty;
    }
}
