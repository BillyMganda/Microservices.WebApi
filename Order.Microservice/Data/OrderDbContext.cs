using Microsoft.EntityFrameworkCore;
using MongoDB.Driver;
using Order.Microservice.Models;

namespace Order.Microservice.Data
{
    public class OrderDbContext : DbContext
    {
        private readonly IMongoDatabase _mongoDatabase;
        public OrderDbContext(DbContextOptions<OrderDbContext> options, IMongoDatabase mongoDatabase) : base(options)
        {
            _mongoDatabase = mongoDatabase;
        }

        public DbSet<OrderEntity> Orders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configure Order entity
            modelBuilder.Entity<OrderEntity>(entity =>
            {
                entity.HasKey(e => e.Id);
                entity.Property(e => e.Id).ValueGeneratedOnAdd();
                entity.Property(e => e.CustomerId).IsRequired();
                entity.Property(e => e.ProductId).IsRequired();
                entity.Property(e => e.Quantity).IsRequired();
                entity.Property(e => e.Price).IsRequired();
                entity.Property(e => e.OrderDate).IsRequired();
            });

            base.OnModelCreating(modelBuilder);
        }

        public IMongoCollection<T> GetCollection<T>(string name)
        {
            return _mongoDatabase.GetCollection<T>(name);
        }
    }
}
