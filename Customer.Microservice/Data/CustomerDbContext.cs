using Customer.Microservice.Models;
using Microsoft.EntityFrameworkCore;

namespace Customer.Microservice.Data
{
    public class CustomerDbContext : DbContext
    {
        public CustomerDbContext(DbContextOptions<CustomerDbContext> options) : base(options)
        {

        }

        public DbSet<CustomerEntity> Customers { get; set; }
    }
}
