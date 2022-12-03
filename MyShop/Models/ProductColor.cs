using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public class ProductColor
    {
        public string Id { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        public string Name { get; set; }

        public List<Image> Images { get; set; } = new List<Image>();
    }
}
