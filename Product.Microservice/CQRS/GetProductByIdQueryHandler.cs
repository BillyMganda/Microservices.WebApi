using MediatR;
using Product.Microservice.DTOs;
using Product.Microservice.Exceptions;
using Product.Microservice.Services;

namespace Product.Microservice.CQRS
{
    public class GetProductByIdQueryHandler : IRequestHandler<GetProductByIdQuery, GetProductDto>
    {
        private readonly IProductRepository _productRepository;
        public GetProductByIdQueryHandler(IProductRepository productRepository)
        {
            _productRepository = productRepository;
        }

        public async Task<GetProductDto> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            var product = await _productRepository.GetByIdAsync(request.Id);

            if (product == null)
            {
                throw new NotFoundException(nameof(GetProductByIdQuery), request.Id);
            }

            var result = new GetProductDto
            {
                Id = product.Id,
                Name = product.Name,
                Description = product.Description,
                Price = product.Price,
                LastModifiedDate = product.LastModifiedDate,
            };

            return result;
        }
    }
}
