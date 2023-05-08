using User.Microservice.Entities;

namespace User.Microservice.Authorization
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(UserModel user);
        public string? ValidateJwtToken(string token);
        public RefreshToken GenerateRefreshToken(string ipAddress);
    }
}
