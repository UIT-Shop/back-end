namespace MyShop.Models
{
    public class OrderInput
    {
        public string? Name { get; set; }
        public string? Phone { get; set; }
        public int WardId { get; set; }
        public string? Street { get; set; }
        public bool? IsPaid { get; set; }
    }
}
