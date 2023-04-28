using MediatR;
using User.Microservice.DTOs;

namespace User.Microservice.CQRS
{
    public class LoginCommand : IRequest<string>
    {
        public LoginDto LoginDto { get; set; }
    }
}
