using User.Microservice.Models;

namespace User.Microservice.Services
{
    public interface IUserRepository : IGenericRepository<UserModel>
    {
    }
}
