using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BasicEcommerce.Models
{
    public class OrderItem
    {
        [Key]
        public Guid OrderItemId { get; set; }
        [ForeignKey(nameof(Order))]
        public Guid OrderId { get; set; }
        public virtual Order? Order{ get; set; }
        [ForeignKey(nameof(Product))]
        public Guid ProductId { get; set; }
        public virtual Product? Product { get; set; }
        public int Quantity { get; set; }
        public decimal Subtotal { get; set; }
    }
}
