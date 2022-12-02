namespace MyShop.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public virtual List<Image> Images { get; set; }
    }
}
