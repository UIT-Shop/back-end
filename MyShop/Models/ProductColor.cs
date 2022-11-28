using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public class ProductColor
    {
        public string Id { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        public Color Color { get; set; }
        public string ColorId { get; set; }

        public List<Image> Images { get; set; } = new List<Image>();
    }
}
