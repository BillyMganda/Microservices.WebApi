using MediatR;
using User.Microservice.Services;

namespace User.Microservice.CQRS
{
    public class ForgotPasswordCommandHandler : IRequestHandler<ForgotPasswordCommand>
    {
        private readonly IUserRepository _userRepository;
        public ForgotPasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public Task Handle(ForgotPasswordCommand request, CancellationToken cancellationToken)
        {
            _userRepository.ForgotPasswordControllerMethod(request.Email);    
            return Task.CompletedTask;
        }
    }
}
