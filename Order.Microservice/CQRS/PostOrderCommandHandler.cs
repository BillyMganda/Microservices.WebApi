using MediatR;
using Order.Microservice.Models;
using Order.Microservice.Services;

namespace Order.Microservice.CQRS
{
    public class PostOrderCommandHandler : IRequestHandler<PostOrderCommand, Guid>
    {
        private readonly IOrderRepository _orderRepository;
        public PostOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<Guid> Handle(PostOrderCommand request, CancellationToken cancellationToken)
        {
            var OrderEntity = new OrderEntity
            {
                Id = Guid.NewGuid(),
                CustomerId = Guid.NewGuid(),
                ProductIds = Guid.NewGuid(), // TODO: Fix This
                OrderDate = DateTime.Now,
                Price = request.Price,
                Quantity = request.Quantity,
            };

            await _orderRepository.CreateAsync(OrderEntity);

            return OrderEntity.Id;
        }
    }
}
