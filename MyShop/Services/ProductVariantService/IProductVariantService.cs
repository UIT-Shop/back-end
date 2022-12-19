namespace MyShop.Services.ProductVariantService
{
    public interface IProductVariantService
    {
        Task<ServiceResponse<List<ProductVariant>>> GetProductVariants(int productId);
        Task<ServiceResponse<ProductVariant>> GetProductVariant(int productId, int productColorId, string productSize);
        Task<ServiceResponse<List<ProductVariant>>> AddProductVariant(ProductVariant productVariant);
        Task<ServiceResponse<List<ProductVariant>>> UpdateProductVariant(ProductVariant productVariant);
        Task<ServiceResponse<bool>> DeleteProductVariant(int productVariantId);
        Task<ServiceResponse<ProductVariant>> GetProductVariant(int productVariantId);
    }
}
