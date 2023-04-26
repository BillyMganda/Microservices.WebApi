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
                CustomerId = Guid.NewGuid(), //TODO: Fix this with logged in customer id from customer microservice
                ProductIds = request.ProductIds, //TODO: Fix this with real product id from product microservice
                OrderDate = DateTime.Now,
                Price = request.Price,
                Quantity = request.Quantity,
            };

            await _orderRepository.CreateAsync(OrderEntity);

            return OrderEntity.Id;
        }
    }
}
