using MediatR;
using Order.Microservice.DTOs;
using Order.Microservice.Exceptions;
using Order.Microservice.Services;

namespace Order.Microservice.CQRS
{
    public class GetOrderByCustomerIdQueryHandler : IRequestHandler<GetOrderByCustomerIdQuery, GetOrderDto>
    {
        private readonly IOrderRepository _orderRepository;

        public GetOrderByCustomerIdQueryHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task<GetOrderDto> Handle(GetOrderByCustomerIdQuery request, CancellationToken cancellationToken)
        {
            var order = await _orderRepository.GetByCustomerIdAsync(request.CustomerId);
            if (order == null)
            {
                throw new NotFoundException(nameof(GetOrderByCustomerIdQuery), request.CustomerId);
            }

            var result = new GetOrderDto
            {
                Id = order.Id,
                CustomerId = order.CustomerId,
                ProductId = order.ProductIds,
                OrderDate = order.OrderDate,
                Price = order.Price,
                Quantity = order.Quantity,
            };

            return result;
        }
    }
}
