namespace MyShop.Models
{
    public class ImageComment
    {
        public int Id { get; set; }
        public Comment? Comment { get; set; }
        public int CommentId { get; set; }
        public string Url { get; set; } = string.Empty;
    }
}
