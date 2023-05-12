using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Distributed;
using Product.Microservice.Data;
using Product.Microservice.Helpers;
using Product.Microservice.Models;

namespace Product.Microservice.Services
{
    public class ProductRepository : IProductRepository
    {
        private readonly ProductDbContext _context;
        private IDistributedCache _cache;
        public ProductRepository(ProductDbContext productDbContext, IDistributedCache cache)
        {
            _context = productDbContext;
            _cache = cache;
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
            var cacheKey = "all_products";
            var cachedProducts = await _cache.GetRecordAsync<IReadOnlyList<ProductEntity>>(cacheKey);

            if (cachedProducts != null)
            {
                return cachedProducts;
            }

            var products = await _context.Products.ToListAsync();

            await _cache.SetRecordAsync(cacheKey, products);

            return products;
        }

        public async Task<ProductEntity> GetByIdAsync(Guid Id)
        {
            var cacheKey = "one_product";
            var cachedProduct = await _cache.GetRecordAsync<ProductEntity>(cacheKey);
            if (cachedProduct != null)
            {
                return cachedProduct;
            }
            var result = await _context.Products.FindAsync(Id);
            await _cache.SetRecordAsync(cacheKey, result);
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
