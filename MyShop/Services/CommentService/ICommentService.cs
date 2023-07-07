namespace MyShop.Services.CommentService
{
    public interface ICommentService
    {
        Task<ServiceResponse<CommentEachPage>> GetComments(int productId, int page);

        Task<ServiceResponse<Comment>> AddComment(Comment comment);

        Task<ServiceResponse<Comment>> UpdateComment(Comment comment);
    }
}
