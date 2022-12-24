namespace MyShop.Services.ProductService
{
    public interface IProductService
    {
        // Create
        Task<ServiceResponse<Product>> CreateProduct(Product product);

        // Read
        Task<ServiceResponse<ProductSearchResult>> GetAdminProducts(int page);
        Task<ServiceResponse<ProductSearchResult>> GetProductsAsync(int page);
        Task<ServiceResponse<Product>> GetProductById(int productId);
        Task<ServiceResponse<ProductSearchResult>> GetProductsByCategory(int categoryId, int page);
        Task<ServiceResponse<ProductSearchResult>> SearchProducts(string searchText, int page);
        Task<ServiceResponse<List<string>>> SearchSuggestionProducts(string searchText);
        Task<ServiceResponse<List<int>>> GetListProductIds();
        Task<ServiceResponse<List<Product>>> GetProducts(List<int> id);

        // Update
        Task<ServiceResponse<Product>> UpdateProduct(Product product);
        Task<bool> UpdateRating(int productId);

        // Delete
        Task<ServiceResponse<bool>> DeleteProduct(int productId);

    }
}
