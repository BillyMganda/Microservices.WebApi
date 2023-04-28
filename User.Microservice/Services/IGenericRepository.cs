using User.Microservice.DTOs;

namespace User.Microservice.Services
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByIdAsync(Guid Id);
        Task<T> GetByEmailAsync(Guid Id);
        Task<T> CreateRefreshToken();
        void CreatePasswordHash(string password, out byte[] PasswordHash, out byte[] PasswordSalt);
        bool VerifyPasswordHash(string password, byte[] PasswordHash, byte[] PasswordSalt);        
        string CreateJWTToken(LoginDto dto);
        string ForgotPaswordToken();
        Task<T> CreateAsync(T entity);
        Task<T> ChangePasswordAsync(T entity);
        Task<T> DeactivateAsync(T entity);
        void SendRegistrationEmail(EmailDto dto, string email, string fname);
    }
}
