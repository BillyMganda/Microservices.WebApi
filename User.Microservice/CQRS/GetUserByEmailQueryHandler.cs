using MediatR;
using User.Microservice.DTOs;
using User.Microservice.Exceptions;
using User.Microservice.Services;

namespace User.Microservice.CQRS
{
    public class GetUserByEmailQueryHandler : IRequestHandler<GetUserByEmailQuery, GetUserDto>
    {
        private readonly IUserRepository _userRepository;
        public GetUserByEmailQueryHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<GetUserDto> Handle(GetUserByEmailQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetUserByEmailAsync(request.Email);
            if (user == null)
            {
                throw new NotFoundException(nameof(User), request.Email);
            }
            return user;
        }
    }
}
