namespace MyShop.Services.BrandService
{
    public interface IBrandService
    {
        // Create
        Task<ServiceResponse<Brand>> CreateBrand(Brand brand);

        // Read
        Task<ServiceResponse<List<Brand>>> GetBrands();

        // Update
        Task<ServiceResponse<Brand>> UpdateBrand(Brand brand);

        // Delete
        Task<ServiceResponse<bool>> DeleteBrand(int brandId);
    }
}
