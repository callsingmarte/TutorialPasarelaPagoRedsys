namespace BasicEcommerce.Models
{
    public class OrderDto
    {
        public Guid OrderId { get; set; }
        public string? RedsysOrderId { get; set; }
        public string? ClientMail { get; set; }
        public decimal TotalPrice { get; set; }
        public string? Status { get; set; }
        public DateTime OrderDate { get; set; }
        public IEnumerable<OrderItemDto>? OrderItems { get; set; }
    }
}
