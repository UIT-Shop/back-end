namespace MyShop.Services.BrandService
{
    public interface IBrandService
    {
        // Create
        Task<ServiceResponse<Brand>> CreateBrand(Brand brand);

        // Read
        Task<ServiceResponse<List<Brand>>> GetBrands();
        Task<ServiceResponse<Brand>> GetBrand(int id);

        // Update
        Task<ServiceResponse<Brand>> UpdateBrand(Brand brand);

        // Delete
        Task<ServiceResponse<bool>> DeleteBrand(int brandId);
    }
}
