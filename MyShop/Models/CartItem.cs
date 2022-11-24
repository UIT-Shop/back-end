namespace MyShop.Models
{
    public class CartItem
    {
        public User? User { get; set; }
        public int UserId { get; set; }
        public ProductVariant? ProductVariant { get; set; }
        public int ProductVariantId { get; set; }
        public int Quantity { get; set; } = 1;
    }
}
