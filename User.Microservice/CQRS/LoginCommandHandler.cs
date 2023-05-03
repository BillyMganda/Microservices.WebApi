using MediatR;
using User.Microservice.DTOs;
using User.Microservice.Services;

namespace User.Microservice.CQRS
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, AuthenticateResponse>
    {
        private readonly IUserRepository _userRepository;
        public LoginCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        public Task<AuthenticateResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var response = _userRepository.Login(request.Email, request.Password);
            return response;
        }
    }
}
