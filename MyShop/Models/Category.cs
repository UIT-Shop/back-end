using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public class Category
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        public string Name { get; set; } = String.Empty;
        public string Url { get; set; } = String.Empty;
        public string Gender { get; set; } = String.Empty;
        public string Type { get; set; } = String.Empty;
        public string MetaTitle { get; set; } = String.Empty;
        public string MetaKeyword { get; set; } = String.Empty;
        public string MetaDiscription { get; set; } = String.Empty;

        public bool Deleted { get; set; } = false;
    }
}
