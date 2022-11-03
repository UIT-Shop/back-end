using System.ComponentModel;

namespace MyShop.Models.MAddress
{
    public class Address
    {
        public int Id { get; set; }
        public int UserId { get; set; }

        public Ward? Ward { get; set; }
        public int WardId { get; set; }

        [DisplayName("FullAddress")]
        public string FullAddress
        {
            get
            {
                return Ward.District.Province.Name + " - " + Ward.District.Prefix + Ward.District.Name + " - " + Ward.Prefix + Ward.Name + " - " + Street;
            }
        }

        public string Province
        {
            get
            {
                return Ward.District.Province.Name;
            }
        }

        public string District
        {
            get
            {
                return Ward.District.Name;
            }
        }

        public string Street { get; set; } = String.Empty;
    }
}
