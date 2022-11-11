namespace MyShop.Services.CategoryService
{
    public class CategoryService : ICategoryService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public CategoryService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<Category>> CreateCategory(Category category)
        {
            if (!_httpContextAccessor.HttpContext.User.IsInRole(Enum.GetName(typeof(Role), Role.Admin)))
                return new ServiceResponse<Category> { Success = false, Message = "You are not allow to do this action" };

            _context.Categories.Add(category);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Category> { Data = category };
        }

        public async Task<ServiceResponse<bool>> DeleteCategory(int categoryId)
        {
            var dbCategory = await _context.Categories.FindAsync(categoryId);
            if (dbCategory == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Category not found."
                };
            }
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<List<Category>>> GetCategories()
        {
            var categories = await _context.Categories.ToListAsync();
            return new ServiceResponse<List<Category>>
            {
                Data = categories
            };
        }

        public async Task<ServiceResponse<Category>> UpdateCategory(Category category)
        {
            var dbCategory = await _context.Categories
                .FirstOrDefaultAsync(p => p.Id == category.Id);

            if (dbCategory == null)
            {
                return new ServiceResponse<Category>
                {
                    Success = false,
                    Message = "Category not found."
                };
            }

            dbCategory.Name = category.Name;
            dbCategory.Url = category.Url;

            await _context.SaveChangesAsync();
            return new ServiceResponse<Category> { Data = category };
        }
    }
}
