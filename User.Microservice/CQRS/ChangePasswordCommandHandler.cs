using MediatR;
using User.Microservice.DTOs;
using User.Microservice.Services;

namespace User.Microservice.CQRS
{
    public class ChangePasswordCommandHandler : IRequestHandler<ChangePasswordCommand, GetUserDto>
    {
        private readonly IUserRepository _userRepository;

        public ChangePasswordCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserDto> Handle(ChangePasswordCommand request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.ChangeUserPasswordAsync(request.Token, request.NewPassword, request.ConfirmNewPassword);
            return user;
        }
    }
}
