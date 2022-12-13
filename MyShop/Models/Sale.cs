using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class Sale
    {
        [Key]
        public DateTime Date { get; set; }

        public float Year { get; set; }

        public float Totals { get; set; } = 0;
    }
}
