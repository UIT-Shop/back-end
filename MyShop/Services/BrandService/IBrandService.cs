namespace MyShop.Services.BrandService
{
    public interface IBrandService
    {
        Task<ServiceResponse<List<Brand>>> GetBrands();
    }
}
