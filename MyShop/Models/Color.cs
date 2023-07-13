namespace MyShop.Models
{
    public class Color
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; } = false;
        public virtual List<Image>? Images { get; set; } = null;
    }
}
