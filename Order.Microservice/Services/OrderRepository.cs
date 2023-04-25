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

        public Task<OrderEntity> DeleteAsync(OrderEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<IReadOnlyList<OrderEntity>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public Task<OrderEntity> GetByCustomerIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderEntity> GetByOrderDateAsync(DateTime date)
        {
            throw new NotImplementedException();
        }

        public Task<OrderEntity> GetByOrderIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderEntity> GetByProductIdAsync(Guid Id)
        {
            throw new NotImplementedException();
        }

        public Task<OrderEntity> UpdateAsync(OrderEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
