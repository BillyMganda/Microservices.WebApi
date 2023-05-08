using Microsoft.EntityFrameworkCore;
using User.Microservice.Entities;

namespace User.Microservice.Helpers
{
    public class DataContext : DbContext
    {
        public DataContext(DbContextOptions<DataContext> options) : base(options)
        {

        }
        public DbSet<UserModel> Users { get; set; }        
    }
}
