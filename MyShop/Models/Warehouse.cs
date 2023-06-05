using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public class Warehouse
    {
        public int Id { get; set; }

        [Column(TypeName = "nvarchar(MAX)")]
        public string Name { get; set; } = String.Empty;

        public virtual Address? Address { get; set; }
        public int AddressId { get; set; }

        public string Phone { get; set; } = String.Empty;

        public bool Deleted { get; set; } = false;
    }
}
