using MongoDB.Bson.Serialization.Attributes;
using MongoDB.Bson;

namespace Order.Microservice.Models
{
    public class OrderEntity
    {
        [BsonId]
        [BsonRepresentation(BsonType.String)]
        public Guid Id { get; set; }
        [BsonElement("CustomerId")]
        public Guid CustomerId { get; set; }
        [BsonElement("ProductId")]
        public List<Guid> ProductIds { get; set; } = new List<Guid>();
        [BsonElement("OrderDate")]
        public DateTime OrderDate { get; set; }
        [BsonElement("Price")]
        public decimal Price { get; set; }
        [BsonElement("Quantity")]
        public int Quantity { get; set; }
    }
}
