using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public class Brand
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        public string Name { get; set; } = String.Empty;
        public string Url { get; set; } = String.Empty;

        public bool Deleted { get; set; } = false;
    }
}
