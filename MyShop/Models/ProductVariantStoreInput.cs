namespace MyShop.Models
{
    public class ProductVariantStoreInput
    {
        public int WarehouseId { get; set; }
        public int? WarehouseFromId { get; set; }
        public int ProductId { get; set; }
        public int ColorId { get; set; }
        public string Size { get; set; }
        public int Quantity { get; set; }
        public decimal BuyPrice { get; set; }
        public DateTime DateInput { get; set; }
        public string LotCode { get; set; }
        public string? Note { get; set; }
    }
}
