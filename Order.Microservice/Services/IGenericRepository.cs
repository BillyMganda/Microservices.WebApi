namespace Order.Microservice.Services
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> GetByOrderIdAsync(Guid Id);
        Task<T> GetByCustomerIdAsync(Guid Id);
        Task<List<T>> GetByProductIdAsync(Guid Id);
        Task<List<T>> GetByOrderDateAsync(DateTime date);
        Task<IReadOnlyList<T>> GetAllAsync();
        Task<T> CreateAsync(T entity);        
        Task<T> DeleteAsync(T entity);
    }
}
