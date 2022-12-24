using static MyShop.Services.CommentService.CommentService;

namespace MyShop.Services.CommentService
{
    public interface ICommentService
    {
        Task<ServiceResponse<List<Comment>>> GetComments(int productId, int page);

        void ReTrainData();

        List<RecommendOutput> GetRecommend(List<int> productId);

        Task<ServiceResponse<Comment>> AddComment(Comment comment);
    }
}
