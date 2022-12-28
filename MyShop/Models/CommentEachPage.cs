namespace MyShop.Models
{
    public class CommentEachPage
    {
        public List<Comment> Comments { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
