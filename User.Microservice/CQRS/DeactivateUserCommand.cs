using MediatR;
using User.Microservice.DTOs;

namespace User.Microservice.CQRS
{
    public class DeactivateUserCommand : IRequest<GetUserDto>
    {
        public Guid Id { get; set; }
    }
}
