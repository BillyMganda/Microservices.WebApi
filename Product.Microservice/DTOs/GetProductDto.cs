namespace Product.Microservice.DTOs
{
    public class GetProductDto
    {
        public Guid Id { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public decimal Price { get; set; }
        public DateTime LastModifiedDate { get; set; }
    }
}
