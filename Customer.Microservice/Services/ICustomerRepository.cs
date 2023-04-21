using Customer.Microservice.Models;

namespace Customer.Microservice.Services
{ 
    public interface ICustomerRepository : IGenericRepository<CustomerEntity>
    {
    }
}
