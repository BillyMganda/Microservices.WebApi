using Order.Microservice.Models;

namespace Order.Microservice.Services
{
    public interface IOrderRepository : IGenericRepository<OrderEntity>
    {
    }
}
