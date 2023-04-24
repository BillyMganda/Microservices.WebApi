using MediatR;
using Product.Microservice.Services;

namespace Product.Microservice.CQRS
{
    public class UpdateProductCommandHandler : IRequestHandler<UpdateProductCommand, string>
    {
        private readonly IProductRepository _repository;
        public UpdateProductCommandHandler(IProductRepository repository)
        {
            _repository = repository;
        }

        public async Task<string> Handle(UpdateProductCommand request, CancellationToken cancellationToken)
        {
            var Product = await _repository.GetByIdAsync(request.Id);
            if (Product == null)
            {
                throw new ArgumentException($"Product with Id {request.Id} not found.");
            }

            Product.Name = request.Name;
            Product.Description = request.Description;
            Product.Price = request.Price;
            Product.LastModifiedDate = DateTime.Now;

            await _repository.UpdateAsync(Product);
            return "Product updated successfully.";
        }
    }
}
