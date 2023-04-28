using MediatR;
using User.Microservice.DTOs;
using User.Microservice.Services;

namespace User.Microservice.CQRS
{
    public class CreateUserCommandHandler : IRequestHandler<CreateUserCommand, GetUserDto>
    {
        private readonly IUserRepository _userRepository;
        public CreateUserCommandHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserDto> Handle(CreateUserCommand request, CancellationToken cancellationToken)
        {
            var UserDto = new AddUserDto
            {
                FirstName = request.Dto.FirstName,
                LastName = request.Dto.LastName,
                Email = request.Dto.Email,
                Password = request.Dto.Password,
                ConfirmPassword = request.Dto.ConfirmPassword,
            };

            var user = await _userRepository.CreateUserAsync(UserDto);  
            return user;
        }
    }
}
