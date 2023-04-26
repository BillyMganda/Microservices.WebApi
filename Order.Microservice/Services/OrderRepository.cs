using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Order.Microservice.Data;
using Order.Microservice.Models;
using SharpCompress.Common;

namespace Order.Microservice.Services
{
    public class OrderRepository : IOrderRepository
    {        
        private readonly IMongoCollection<OrderEntity> _ordersCollection;
        public OrderRepository(IOptions<OrdersDatabaseSettings> OrderStoreDatabaseSettings)
        {
            var mongoClient = new MongoClient(OrderStoreDatabaseSettings.Value.ConnectionString);
            var mongoDatabase = mongoClient.GetDatabase(OrderStoreDatabaseSettings.Value.DatabaseName);
            _ordersCollection = mongoDatabase.GetCollection<OrderEntity>(OrderStoreDatabaseSettings.Value.OrdersCollectionName);
        }

        public async Task<OrderEntity> CreateAsync(OrderEntity entity)
        {
            await _ordersCollection.InsertOneAsync(entity);
            return entity;
        }

        public async Task<OrderEntity> DeleteAsync(OrderEntity entity)
        {
            var result = await _ordersCollection.DeleteOneAsync(x => x.Id == entity.Id);
            return result.DeletedCount > 0 ? entity : null;
        }

        public async Task<IReadOnlyList<OrderEntity>> GetAllAsync()
        {
            var orders = await _ordersCollection.Find(_ => true).ToListAsync();
            return orders;
        }

        public async Task<OrderEntity> GetByCustomerIdAsync(Guid Id)
        {
            var order = await _ordersCollection.Find(x => x.CustomerId == Id).FirstOrDefaultAsync();
            return order;
        }

        public async Task<List<OrderEntity>> GetByOrderDateAsync(DateTime date)
        {
            var order = await _ordersCollection.Find(x => x.OrderDate == date).ToListAsync();
            return order;
        }

        public async Task<OrderEntity> GetByOrderIdAsync(Guid Id)
        {
            var order = await _ordersCollection.Find(x => x.Id == Id).FirstOrDefaultAsync();
            return order;
        }

        public async Task<List<OrderEntity>> GetByProductIdAsync(Guid Id)
        {
            var result = await _context.Orders.Where(x => x.ProductIds.Contains(Id)).ToListAsync();
            return result;

            var order = await _ordersCollection.Find(x => x.ProductIds == Id).ToListAsync();
            return order;
        }        
    }
}
