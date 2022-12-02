namespace MyShop.Models
{
    public class Image
    {
        public int Id { get; set; }
        public Color? Color { get; set; }
        public int? ColorId { get; set; }
        public Product? Product { get; set; }
        public int ProductId { get; set; }
        public string Url { get; set; } = string.Empty;
    }
}
