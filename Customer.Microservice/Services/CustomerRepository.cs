using Customer.Microservice.Data;
using Customer.Microservice.Models;
using Microsoft.EntityFrameworkCore;

namespace Customer.Microservice.Services
{
    public class CustomerRepository : ICustomerRepository
    {
        private readonly CustomerDbContext _context;
        public CustomerRepository(CustomerDbContext context)
        {
            _context = context;
        }

        public Task<CustomerEntity> CreateAsync(CustomerEntity entity)
        {
            throw new NotImplementedException();
        }

        public Task<CustomerEntity> DeleteAsync(CustomerEntity entity)
        {
            throw new NotImplementedException();
        }

        public async Task<IReadOnlyList<CustomerEntity>> GetAllAsync()
        {
            return await _context.Customers.ToListAsync();
        }

        public async Task<CustomerEntity> GetByIdAsync(Guid Id)
        {
            var result = await _context.Customers.FindAsync(Id);
            return result;
        }

        public Task<CustomerEntity> UpdateAsync(CustomerEntity entity)
        {
            throw new NotImplementedException();
        }
    }
}
