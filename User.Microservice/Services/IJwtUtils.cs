using User.Microservice.Models;

namespace User.Microservice.Services
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(UserModel user);
        public string? ValidateJwtToken(string token);
        public RefreshToken GenerateRefreshToken(string ipAddress);
    }
}
