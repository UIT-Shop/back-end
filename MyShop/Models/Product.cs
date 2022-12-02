using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public class Product
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        public string Title { get; set; } = String.Empty;
        public string Description { get; set; } = String.Empty;

        public Category? Category { get; set; }
        public int CategoryId { get; set; }

        public Brand? Brand { get; set; }
        public int BrandId { get; set; }

        public List<ProductVariant> Variants { get; set; } = new List<ProductVariant>();
        public virtual List<Image> Images { get; set; } = new List<Image>();

        public string MetaTitle { get; set; } = String.Empty;
        public string MetaKeyword { get; set; } = String.Empty;
        public string MetaDiscription { get; set; } = String.Empty;

        public bool Visible { get; set; } = true;
        public bool Deleted { get; set; } = false;
        [NotMapped]
        public bool Editing { get; set; } = false;
        [NotMapped]
        public bool IsNew { get; set; } = false;
    }
}
