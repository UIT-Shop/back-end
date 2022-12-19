using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }
        public ProductVariant? ProductVariant { get; set; }
        public int ProductVariantId { get; set; }
        public int? ProductId { get; set; } = 1;
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CommentDate { get; set; } = DateTime.Now;

        public string Content { get; set; } = String.Empty;
        public float Rating { get; set; } = 5f;
        public List<ImageComment> ImageComments { get; set; }
    }
}
