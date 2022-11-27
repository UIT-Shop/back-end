namespace MyShop.Services.CategoryService
{
    public interface ICategoryService
    {
        // Create
        Task<ServiceResponse<Category>> CreateCategory(Category category);

        // Read
        Task<ServiceResponse<List<Category>>> GetCategories();
        Task<ServiceResponse<Category>> GetCategory(int id);

        // Update
        Task<ServiceResponse<Category>> UpdateCategory(Category category);

        // Delete
        Task<ServiceResponse<bool>> DeleteCategory(int categoryId);
    }
}
