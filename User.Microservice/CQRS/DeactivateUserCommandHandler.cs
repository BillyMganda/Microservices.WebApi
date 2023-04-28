using MediatR;
using User.Microservice.DTOs;
using User.Microservice.Services;

namespace User.Microservice.CQRS
{
    public class DeactivateUserCommandHandler : IRequestHandler<DeactivateUserCommand, GetUserDto>
    {
        private readonly IUserRepository _userRepository;
        public DeactivateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserDto> Handle(DeactivateUserCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.DeactivateUserAsync(request.Id);
            return user;
        }
    }
}
