namespace MyShop.Services.CommentService
{
    public class CommentService : ICommentService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;
        private readonly IUserService _userService;
        private readonly IProductService _productService;
        private readonly IRatingService _ratingService;

        public CommentService(DataContext context, IAuthService authService, IUserService userService, IProductService productService, IRatingService ratingService)
        {
            _context = context;
            _authService = authService;
            _userService = userService;
            _productService = productService;
            _ratingService = ratingService;
        }

        public async Task<ServiceResponse<CommentEachPage>> GetComments(int productId, int page)
        {
            var allComments = await _context.Comments.Where(c => c.ProductId == productId).ToListAsync();
            var pageResults = 10f;
            var pageCount = Math.Ceiling(allComments.Count / pageResults);
            var comments = allComments
                .OrderByDescending(c => c.CommentDate)
                .Skip((page - 1) * (int)pageResults)
                .Take((int)pageResults)
                .ToList();
            return new ServiceResponse<CommentEachPage>
            {
                Data = new CommentEachPage()
                {
                    Comments = comments,
                    Pages = Convert.ToInt32(pageCount),
                    CurrentPage = page
                }
            };
        }

        public async Task<ServiceResponse<Comment>> AddComment(Comment comment)
        {
            var userId = _authService.GetUserId();
            var userInfo = await _userService.GetUserInfo(userId);
            var productVariant = await _context.ProductVariants
                .Include(pv => pv.Color).Include(pv => pv.Product)
                .Where(pv => pv.Id == comment.ProductVariantId)
                .FirstOrDefaultAsync();
            if (productVariant == null)
                return new ServiceResponse<Comment>
                {
                    Data = null,
                    Success = false,
                    Message = "Variant not found"
                };
            comment.ProductId = productVariant.ProductId;
            comment.ProductColor = productVariant.Color.Name;
            comment.ProductSize = productVariant.ProductSize;
            comment.ProductTitle = productVariant.Product.Title;
            comment.UserName = userInfo.Data.Name;
            comment.UserId = userId;
            _context.Comments.Add(comment);

            await _context.SaveChangesAsync();

            var listCommentOfProducts = await _context.Comments
                .Where(c => c.ProductId == comment.ProductId)
                .ToListAsync();

            await _productService.UpdateRating(productVariant.ProductId, listCommentOfProducts);
            await _ratingService.AddOrUpdateRating(new RatingPerProduct() { ProductId = comment.ProductId, UserId = comment.UserId, Rating = comment.Rating }, listCommentOfProducts);

            return new ServiceResponse<Comment> { Data = comment };
        }
    }
}
