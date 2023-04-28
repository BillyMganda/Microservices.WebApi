using Microsoft.EntityFrameworkCore;
using User.Microservice.Data;
using User.Microservice.DTOs;
using User.Microservice.Models;
using User.Microservice.Exceptions;

namespace User.Microservice.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;
        public UserRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GetUserDto> ChangeUserPasswordAsync(ChangePasswordDto entity, byte[] Hash, byte[] Salt)
        {
            var user = await _dbContext.Users.FirstOrDefaultAsync(x => x.ForgotPasswordToken == entity.Token);
            if (user == null)
                throw new NotFoundException(nameof(user), entity.Token);

            user.PasswordHash = Hash;
            user.PasswordSalt = Salt;
            user.ForgotPasswordToken = "";
            await _dbContext.SaveChangesAsync();

            var GetDto = new GetUserDto
            {
                Id = user.Id,
                FirstName = user.FirstName,
                LastName = user.LastName,
                Email = user.Email,
                CreatedDate = DateTime.Now,
            };
            return GetDto;
        }

        public string CreateJWTToken(LoginDto dto)
        {
            throw new NotImplementedException();
        }

        public void CreatePasswordHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            throw new NotImplementedException();
        }

        public Task<GetUserDto> CreateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public Task<GetUserDto> CreateUserAsync(AddUserDto entity)
        {
            throw new NotImplementedException();
        }

        public Task<GetUserDto> DeactivateUserAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public string ForgotPaswordToken()
        {
            throw new NotImplementedException();
        }

        public Task<GetUserDto> GetUserByEmailAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<GetUserDto> GetUserByIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public void SendForgotPasswordEmail(EmailDto dto)
        {
            throw new NotImplementedException();
        }

        public void SendRegistrationEmail(EmailDto dto)
        {
            throw new NotImplementedException();
        }

        public bool VerifyPasswordHash(string password, byte[] PasswordHash, byte[] PasswordSalt)
        {
            throw new NotImplementedException();
        }
    }
}
