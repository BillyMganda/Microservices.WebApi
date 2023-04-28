using User.Microservice.DTOs;

namespace User.Microservice.Services
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetUserByIdAsync(Guid Id);
        Task<T> GetUserByEmailAsync(Guid Id);
        Task<T> CreateRefreshToken();
        void CreatePasswordHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt);
        bool VerifyPasswordHash(string password, byte[] PasswordHash, byte[] PasswordSalt);        
        string CreateJWTToken(LoginDto dto);
        string ForgotPaswordToken();
        Task<T> CreateUserAsync(T entity);
        Task<T> ChangeUserPasswordAsync(T entity);
        Task<T> DeactivateUserAsync(T entity);
        void SendRegistrationEmail(EmailDto dto);
        void SendForgotPasswordEmail(EmailDto dto);
    }
}
