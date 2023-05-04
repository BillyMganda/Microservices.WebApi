using User.Microservice.Models;

namespace User.Microservice.Services
{
    public interface IJwtUtils
    {
        public string GenerateJwtToken(UserModel user);
        public int? ValidateJwtToken(string token);
        public RefreshToken GenerateRefreshToken(string ipAddress);
    }
}
