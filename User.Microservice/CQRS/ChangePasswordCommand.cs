using MediatR;
using User.Microservice.DTOs;

namespace User.Microservice.CQRS
{
    public class ChangePasswordCommand : IRequest<GetUserDto>
    {
        public ChangePasswordDto changePassword { get; set; }
        public byte[] PasswordHash { get; set; } = new byte[32];
        public byte[] PasswordSalt { get; set; } = new byte[32];
    }
}
