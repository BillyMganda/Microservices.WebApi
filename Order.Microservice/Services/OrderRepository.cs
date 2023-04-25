using Microsoft.EntityFrameworkCore;
using Order.Microservice.Data;
using Order.Microservice.Models;

namespace Order.Microservice.Services
{
    public class OrderRepository : IOrderRepository
    {
        private readonly OrderDbContext _context;
        public OrderRepository(OrderDbContext dbContext)
        {
            _context = dbContext;
        }

        public async Task<OrderEntity> CreateAsync(OrderEntity entities)
        {
            await _context.Orders.AddRangeAsync(entities);
            await _context.SaveChangesAsync();

            return entities;
        }

        public async Task<OrderEntity> DeleteAsync(OrderEntity entity)
        {
            var result = await _context.Orders.FirstOrDefaultAsync(x => x.Id == entity.Id);
            _context.Orders.Remove(result!);
            await _context.SaveChangesAsync();
            return result;
        }

        public async Task<IReadOnlyList<OrderEntity>> GetAllAsync()
        {
            return await _context.Orders.ToListAsync();
        }

        public async Task<OrderEntity> GetByCustomerIdAsync(Guid Id)
        {
            var result = await _context.Orders.FindAsync(Id);
            return result;
        }

        public async Task<List<OrderEntity>> GetByOrderDateAsync(DateTime date)
        {
            var result = await _context.Orders.Where(x => x.OrderDate == date).ToListAsync();
            return result;
        }

        public async Task<OrderEntity> GetByOrderIdAsync(Guid Id)
        {
            var result = await _context.Orders.FindAsync(Id);
            return result;
        }

        public async Task<List<OrderEntity>> GetByProductIdAsync(Guid Id)
        {
            var result = await _context.Orders.Where(x => x.ProductIds.Contains(Id)).ToListAsync();
            return result;
        }        
    }
}
