namespace Order.Microservice.DTOs
{
    public class GetOrderDto
    {
        public Guid Id { get; set; }
        public Guid CustomerId { get; set; }
        public List<Guid> ProductId { get; set; } = new List<Guid>();
        public DateTime OrderDate { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
    }
}
