namespace MyShop.Services.ProductVariantStoreService
{
    public interface IProductVariantStoreService
    {
        Task<ServiceResponse<List<ProductVariantStoreOutput>>> GetProductVariantStoresByProduct(int productId, DateTime monthYear);
        Task<ServiceResponse<List<ProductVariantStoreOutput>>> GetProductVariantStoresByWarehouse(int warehouseId, DateTime monthYear);
        Task<ServiceResponse<bool>> AddProductVariantStore(ProductVariantStoreInput productVariantStoreInput);
        Task<ServiceResponse<bool>> MoveProductVariantStore(ProductVariantStoreInput productVariantStoreInput);
        Task<ServiceResponse<bool>> UpdateProductVariantStore(ProductVariantStore productVariantStore);
        Task<ServiceResponse<bool>> DeleteProductVariantStore(int productVariantStoreId);
        Task<ServiceResponse<ProductVariantStore>> GetProductVariantStore(int productVariantStoreId);
    }
}
