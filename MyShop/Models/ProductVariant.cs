using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyShop.Models
{
    public class ProductVariant
    {
        public int Id { get; set; }

        [JsonIgnore]
        public Product? Product { get; set; }
        public int ProductId { get; set; }

        public Color? Color { get; set; }
        public int ColorId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OriginalPrice { get; set; }

        public int Quantity { get; set; }
        public string ProductSize { get; set; }

        public bool Visible { get; set; } = true;
        public bool Deleted { get; set; } = false;
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
