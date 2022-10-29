namespace MyShop.Models
{
    public class ProductSearchResult
    {
        public List<Product> Products { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
