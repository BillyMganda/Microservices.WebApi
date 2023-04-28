using User.Microservice.DTOs;

namespace User.Microservice.Services
{
    public interface IUserRepository
    {
        Task<GetUserDto> GetUserByIdAsync(Guid Id);
        Task<GetUserDto> GetUserByEmailAsync(string Email);
        string CreateRefreshToken();
        void CreatePasswordHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt);
        bool VerifyPasswordHash(string password, byte[] PasswordHash, byte[] PasswordSalt);
        string CreateJWTToken(LoginDto dto);
        string ForgotPaswordToken();
        Task<GetUserDto> CreateUserAsync(AddUserDto entity);
        Task<GetUserDto> ChangeUserPasswordAsync(ChangePasswordDto entity, byte[] Hash, byte[] Salt);
        Task<GetUserDto> DeactivateUserAsync(Guid Id);
        void SendRegistrationEmail(EmailDto dto);
        void SendForgotPasswordEmail(EmailDto dto);
    }
}
