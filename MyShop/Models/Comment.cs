using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class Comment
    {
        public int Id { get; set; }
        public User? User { get; set; }
        public int UserId { get; set; }
        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:HH:mm:ss dd/MM/yyyy}", ApplyFormatInEditMode = true)]
        public DateTime CommentDate { get; set; } = DateTime.Now;

        public string Content { get; set; }
        public int Rating { get; set; }
        public List<ImageComment> ImageComments { get; set; }
    }
}
