using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json.Serialization;

namespace MyShop.Models
{
    public class ProductVariant
    {
        [JsonIgnore]
        public Product? Product { get; set; }
        public int ProductId { get; set; }

        public ProductColor? ProductColor { get; set; }
        public string ProductColorId { get; set; }

        public ProductType? ProductType { get; set; }
        public int? ProductTypeId { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal Price { get; set; }

        [Column(TypeName = "decimal(18,2)")]
        public decimal OriginalPrice { get; set; }

        public List<Image> Images { get; set; } = new List<Image>();

        public bool Visible { get; set; } = true;
        public bool Deleted { get; set; } = false;
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
