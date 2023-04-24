using MediatR;
using Product.Microservice.Models;
using Product.Microservice.Services;

namespace Product.Microservice.CQRS
{
    public class AddProductCommandHandler : IRequestHandler<AddProductCommand, Guid>
    {
        private readonly IProductRepository _productRepository;
        public AddProductCommandHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<Guid> Handle(AddProductCommand request, CancellationToken cancellationToken)
        {
            var ProductEntity = new ProductEntity
            {
                Id = Guid.NewGuid(),
                Name = request.Name,
                Description = request.Description,
                Price = request.Price,
                DateCreated = DateTime.Now,
                LastModifiedDate = DateTime.Now,
            };

            await _productRepository.CreateAsync(ProductEntity);
            return ProductEntity.Id;
        }
    }
}
