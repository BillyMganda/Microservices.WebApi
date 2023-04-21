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

        public async Task<CustomerEntity> DeleteAsync(CustomerEntity entity)
        {
            var result = await _context.Customers.FirstOrDefaultAsync(x => x.Id == entity.Id);
            _context.Customers.Remove(result);
            await _context.SaveChangesAsync();
            return result;
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

        public async Task<CustomerEntity> UpdateAsync(CustomerEntity entity)
        {
            var existingEntity = await _context.Customers.FindAsync(entity.Id);
            if (existingEntity == null)
            {
                throw new Exception($"Customer with id {entity.Id} not found.");
            }

            existingEntity.FirstName = entity.FirstName;
            existingEntity.LastName = entity.LastName;
            existingEntity.Email = entity.Email;
            existingEntity.PhoneNumber = entity.PhoneNumber;
            existingEntity.City = entity.City;
            existingEntity.State = entity.State;
            existingEntity.ZipCode = entity.ZipCode;
            existingEntity.LastModifiedDate = DateTime.UtcNow;

            await _context.SaveChangesAsync();

            return existingEntity;
        }
    }
}
