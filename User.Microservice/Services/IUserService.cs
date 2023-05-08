using User.Microservice.DTOs;
using User.Microservice.Entities;

namespace User.Microservice.Services
{
    public interface IUserService
    {
        AuthenticateResponse Authenticate(AuthenticateRequest model, string ipAddress);
        AuthenticateResponse RefreshToken(string token, string ipAddress);
        void RevokeToken(string token, string ipAddress);
        IEnumerable<UserModel> GetAll();
        UserModel GetById(Guid id);

        string AddNewUser(UserModel model);
    }
}
