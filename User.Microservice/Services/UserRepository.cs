using User.Microservice.Data;
using User.Microservice.DTOs;
using User.Microservice.Models;

namespace User.Microservice.Services
{
    public class UserRepository : IUserRepository
    {
        private readonly UserDbContext _dbContext;
        public UserRepository(UserDbContext dbContext)
        {
            _dbContext = dbContext;
        }
               
    }
}
