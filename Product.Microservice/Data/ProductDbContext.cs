using Microsoft.EntityFrameworkCore;
using Product.Microservice.Models;

namespace Product.Microservice.Data
{
    public class ProductDbContext : DbContext
    {
        public ProductDbContext(DbContextOptions<ProductDbContext> options) : base(options)
        {

        }

        public DbSet<ProductEntity> Products { get; set; }
    }
}
