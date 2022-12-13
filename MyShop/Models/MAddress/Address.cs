using System.ComponentModel;

namespace MyShop.Models.MAddress
{
    public class Address
    {
        public int Id { get; set; }

        public Ward? Ward { get; set; }
        public int WardId { get; set; }

        [DisplayName("FullAddress")]
        public string FullAddress => Street + " " + Ward?.Prefix + " " + Ward?.Name + " - " + District + " - " + Province;

        public string Province => Ward?.District?.Province?.Name ?? "";

        public string District => Ward?.District?.Prefix + " " + Ward?.District?.Name ?? "";

        public string Street { get; set; } = String.Empty;
    }
}
