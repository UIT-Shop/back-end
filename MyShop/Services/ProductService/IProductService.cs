namespace MyShop.Services.ProductService
{
    public interface IProductService
    {
        // Create
        Task<ServiceResponse<Product>> CreateProduct(Product product);

        // Read
        Task<ServiceResponse<List<Product>>> GetAdminProducts();
        Task<ServiceResponse<List<Product>>> GetProductsAsync();
        Task<ServiceResponse<Product>> GetProductById(int productId);
        Task<ServiceResponse<List<Product>>> GetProductsByCategory(string categoryUrl);
        Task<ServiceResponse<ProductSearchResult>> SearchProducts(string searchText, int page);
        Task<ServiceResponse<List<string>>> SearchSuggestionProducts(string searchText);

        // Update
        Task<ServiceResponse<Product>> UpdateProduct(Product product);

        // Delete
        Task<ServiceResponse<bool>> DeleteProduct(int productId);

    }
}
