using MediatR;
using User.Microservice.DTOs;
using User.Microservice.Services;

namespace User.Microservice.CQRS
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand, GetUserDto>
    {
        private readonly IUserRepository _userRepository;
        public ForgotPasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }
        async Task<GetUserDto> IRequestHandler<ForgotPasswordCommand, GetUserDto>.Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.ForgotPasswordControllerMethod(request.Email);
            return user;
        }
    }
}
