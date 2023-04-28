using User.Microservice.Data;
using User.Microservice.DTOs;
using User.Microservice.Models;

namespace User.Microservice.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;
        public UserRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public Task<UserModel> ChangeUserPasswordAsync(UserModel entity)
        {
            throw new NotImplementedException();
        }

        public string CreateJWTToken(LoginDto dto)
        {
            throw new NotImplementedException();
        }

        public void CreatePasswordHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> CreateRefreshToken()
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> CreateUserAsync(UserModel entity)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> DeactivateUserAsync(UserModel entity)
        {
            throw new NotImplementedException();
        }

        public string ForgotPaswordToken()
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetUserByEmailAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<UserModel> GetUserByIdAsync(Guid Id)
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
