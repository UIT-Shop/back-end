using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public string UserName { get; set; } = String.Empty;
        public int UserId { get; set; }
        public int ProductVariantId { get; set; }
        public string ProductTitle { get; set; } = String.Empty;
        public string ProductSize { get; set; } = String.Empty;
        public string ProductColor { get; set; } = String.Empty;
        public int ProductId { get; set; } = 1;
        public int OrderItemId { get; set; } = 1;

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CommentDate { get; set; } = DateTime.Now;

        public string Content { get; set; } = String.Empty;
        public float Rating { get; set; } = 5f;
        public List<ImageComment>? ImageComments { get; set; }
    }
}
