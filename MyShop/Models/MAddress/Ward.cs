using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models.MAddress
{
    public class Ward
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        public string Name { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        public string Prefix { get; set; }

        public District? District { get; set; }
        public int DistrictId { get; set; }

        public int ProvinceId { get; set; }
    }
}
