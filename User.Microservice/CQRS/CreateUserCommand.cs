using MediatR;
using User.Microservice.DTOs;

namespace User.Microservice.CQRS
{
    public class CreateUserCommand : IRequest<GetUserDto>
    {
        public AddUserDto Dto { get; set; }
    }
}
