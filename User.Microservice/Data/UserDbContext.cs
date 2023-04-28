using Microsoft.EntityFrameworkCore;
using User.Microservice.Models;

namespace User.Microservice.Data
{
    public class UserDbContext : DbContext
    {
        public UserDbContext(DbContextOptions<UserDbContext> options) : base(options)
        {

        }

        public DbSet<UserModel> Users { get; set; }
    }
}
