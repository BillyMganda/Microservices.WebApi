using MediatR;
using Order.Microservice.Services;
using Order.Microservice.Validations;

namespace Order.Microservice.CQRS
{
    public class DeleteOrderCommandHandler : IRequestHandler<DeleteOrderCommand>
    {
        private readonly IOrderRepository _orderRepository;
        public DeleteOrderCommandHandler(IOrderRepository orderRepository)
        {
            _orderRepository = orderRepository;
        }

        public async Task Handle(DeleteOrderCommand request, CancellationToken cancellationToken)
        {
            var validator = new DeleteOrderCommandValidator();
            var validationResult = await validator.ValidateAsync(request);
            if (validationResult.IsValid == false)
                throw new Exception();

            var order = await _orderRepository.GetByOrderIdAsync(request.OrderId);
            if (order == null)
            {
                throw new ArgumentException($"Crder with Id {request.OrderId} not found.");
            }
            await _orderRepository.DeleteAsync(order);
        }
    }
}
 