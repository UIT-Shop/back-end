namespace MyShop.Models
{
    public class ProductVariantStoreOutput
    {
        public int? WarehouseId { get; set; }
        public string? WarehouseName { get; set; }
        public int? ProductId { get; set; }
        public string? ProductName { get; set; }
        public string Color { get; set; }
        public string Size { get; set; }
        public int? QuantityIn { get; set; }
        public int? QuantityOut { get; set; }
        public int QuantityCurrent { get; set; } = 0;
        public DateTime LastDate { get; set; }
    }
}
