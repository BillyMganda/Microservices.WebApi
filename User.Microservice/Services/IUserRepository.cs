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
        string CreateJWTToken(string Email, string Password);
        string ForgotPaswordToken();
        Task<AuthenticateResponse> Login(string Email, string Password);
        Task<GetUserDto> CreateUserAsync(AddUserDto entity);
        Task<GetUserDto> ChangeUserPasswordAsync(string Token, string NewPassword, string ConfirmNewPassword);
        Task<GetUserDto> DeactivateUserAsync(Guid Id);
        void SendRegistrationEmail(EmailDto dto);
        void SendForgotPasswordEmail(string Email, string Token);
        Task<GetUserDto> ForgotPasswordControllerMethod(string Email);
    }
}
