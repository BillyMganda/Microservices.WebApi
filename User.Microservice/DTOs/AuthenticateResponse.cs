using System.Text.Json.Serialization;
using User.Microservice.Models;

namespace User.Microservice.DTOs
{
    public class AuthenticateResponse
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string JwtToken { get; set; }
        [JsonIgnore] // refresh token is returned in http only cookie
        public string RefreshToken { get; set; }

        public AuthenticateResponse(UserModel user, string jwtToken, string refreshToken)
        {
            Id = user.Id;
            FirstName = user.FirstName;
            LastName = user.LastName;
            JwtToken = jwtToken;
            RefreshToken = refreshToken;
        }
    }
}
    