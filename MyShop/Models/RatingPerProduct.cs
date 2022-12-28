using Microsoft.ML.Data;

namespace MyShop.Models
{
    public class RatingPerProduct
    {
        [ColumnName(@"UserId")]
        public int UserId { get; set; }

        [ColumnName(@"Rating")]
        public float Rating { get; set; } = 5f;

        [ColumnName(@"ProductId")]
        public int ProductId { get; set; }
    }
}
