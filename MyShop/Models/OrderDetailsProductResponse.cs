using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public class OrderDetailsProductResponse
    {
        public int ProductId { get; set; }
        public string Title { get; set; }
        public string ProductSize { get; set; }
        public string ProductColor { get; set; }
        public int ProductVariantID { get; set; }
        public string ImageUrl { get; set; }
        public int Quantity { get; set; }
        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
    }
}
