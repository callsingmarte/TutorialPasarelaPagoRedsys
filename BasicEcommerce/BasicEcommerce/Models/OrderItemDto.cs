using System.ComponentModel.DataAnnotations.Schema;

namespace BasicEcommerce.Models
{
    public class OrderItemDto
    {
        public Guid OrderItemId { get; set; }
        public Guid ProductId { get; set; }
        public ProductDto? Product { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
    }
}
