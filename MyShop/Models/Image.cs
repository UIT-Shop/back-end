namespace MyShop.Models
{
    public class Image
    {
        public int Id { get; set; }
        public ProductColor? ProductColor { get; set; }
        public int ProductColorId { get; set; }
        public string Data { get; set; } = string.Empty;
    }
}
