using System.ComponentModel.DataAnnotations;

namespace BasicEcommerce.Models
{
    public class Order
    {
        [Key]
        public Guid OrderId { get; set; }
        public string? RedsysOrderId { get; set; }
        public string?  ClientMail { get; set; }
        public decimal TotalPrice { get; set; }
        public string Status { get; set; } = "PagoPendiente";
        public DateTime OrderDate { get; set; }
        public virtual IEnumerable<OrderItem>? OrderItems { get; set; } = new List<OrderItem>();
    }
}
