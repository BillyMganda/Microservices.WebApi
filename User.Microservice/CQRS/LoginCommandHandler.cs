using MediatR;
using User.Microservice.Services;

namespace User.Microservice.CQRS
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, string>
    {
        private readonly IUserRepository _userRepository;
        public LoginCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task<string> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var Jwt = _userRepository.Login(request.Email, request.Password);
            return Jwt;
        }
    }
}
