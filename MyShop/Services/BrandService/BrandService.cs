namespace MyShop.Services.BrandService
{
    public class BrandService : IBrandService
    {
        private readonly DataContext _context;

        public BrandService(DataContext context)
        {
            _context = context;
        }

        public async Task<ServiceResponse<List<Brand>>> GetBrands()
        {
            var brands = await _context.Brands.ToListAsync();
            return new ServiceResponse<List<Brand>>
            {
                Data = brands
            };
        }
    }
}
