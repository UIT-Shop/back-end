using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyShop.Models
{
    public class ProductVariantStore
    {
        public int Id { get; set; }

        [JsonIgnore]
        public ProductVariant? ProductVariant { get; set; }
        public int ProductVariantId { get; set; }

        public Warehouse? Warehouse { get; set; }
        public int WarehouseId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal BuyPrice { get; set; }

        public int Quantity { get; set; }

        public int Stock { get; set; }

        public bool Deleted { get; set; } = false;

        public DateTime DateInput { get; set; }

        public string LotCode { get; set; }
        public string? Note { get; set; }
    }
}
