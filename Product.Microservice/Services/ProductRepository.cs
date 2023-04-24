using Microsoft.EntityFrameworkCore;
using Product.Microservice.Data;
using Product.Microservice.Models;

namespace Product.Microservice.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;
        public ProductRepository(ProductDbContext productDbContext)
        {
            _context = productDbContext;
        }

        public async Task<ProductEntity> CreateAsync(ProductEntity entity)
        {
            await _context.Products.AddAsync(entity);
            await _context.SaveChangesAsync();

            return entity;
        }

        public async Task<ProductEntity> DeleteAsync(ProductEntity entity)
        {
            var result = await _context.Products.FirstOrDefaultAsync(x => x.Id == entity.Id);
            _context.Products.Remove(result);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<IReadOnlyList<ProductEntity>> GetAllAsync()
        {
            return await _context.Products.ToListAsync();
        }

        public async Task<ProductEntity> GetByIdAsync(Guid Id)
        {
            var result = await _context.Products.FindAsync(Id);
            return result;
        }

        public async Task<ProductEntity> UpdateAsync(ProductEntity entity)
        {
            var existingEntity = await _context.Products.FindAsync(entity.Id);
            if (existingEntity == null)
            {
                throw new Exception($"Product with id {entity.Id} not found.");
            }

            existingEntity.Name = entity.Name;
            existingEntity.Description = entity.Description;
            existingEntity.Price = entity.Price;            
            existingEntity.LastModifiedDate = DateTime.Now;

            await _context.SaveChangesAsync();

            return existingEntity;
        }
    }
}
