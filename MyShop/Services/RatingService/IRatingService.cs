using static MyShop.Services.RatingService.RatingService;

namespace MyShop.Services.RatingService
{
    public interface IRatingService
    {
        void ReTrainData();

        List<RecommendOutput> GetRecommend(List<int> productId);

        Task<ServiceResponse<RatingPerProduct>> AddOrUpdateRating(RatingPerProduct rating, List<Comment> listComment);

        Task<ServiceResponse<List<RatingCount>>> GetSummaryRating(int productId);
    }
}
