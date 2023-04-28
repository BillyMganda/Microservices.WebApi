using MediatR;
using User.Microservice.DTOs;

namespace User.Microservice.CQRS
{
    public class GetUserByEmailQuery : IRequest<GetUserDto>
    {
        public string Email { get; set; } = string.Empty;
    }
}
