namespace BasicEcommerce.Models
{
    public class PurchaseRequestDto
    {
        public Guid ProductId { get; set; }
        public int Quantity { get; set; } = 1;
        public string? UserMail { get; set; }
    }
}
