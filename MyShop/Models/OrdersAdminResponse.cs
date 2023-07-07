namespace MyShop.Models
{
    public class OrdersAdminResponse
    {
        public List<OrderOverviewResponse> OrderOverviews { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
