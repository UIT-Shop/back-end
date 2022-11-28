namespace MyShop.Models
{
    public class ProductColor
    {
        public int Id { get; set; }

        public Color? Color { get; set; }
        public string ColorId { get; set; }

        public List<Image> Images { get; set; }
    }
}