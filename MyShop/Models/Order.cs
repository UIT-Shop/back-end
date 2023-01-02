using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public enum Status
    {
        Waiting,
        Received,
        Delivering,
        Delivered,
        Cancelled
    }

    public class Order
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }
        public string Name { get; set; } = string.Empty;
        public string Phone { get; set; } = string.Empty;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime OrderDate { get; set; } = DateTime.Now;

        [Column(TypeName = "decimal(18,2)")]
        public decimal TotalPrice { get; set; }
        public Status Status { get; set; } = Status.Waiting;
        public List<OrderItem> OrderItems { get; set; }

        public virtual Address? Address { get; set; }
        public int AddressId { get; set; }
    }
}
