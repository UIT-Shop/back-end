namespace MyShop.Models
{
    public class UserSearchResult
    {
        public List<User> Users { get; set; }
        public int Pages { get; set; }
        public int CurrentPage { get; set; }
    }
}
