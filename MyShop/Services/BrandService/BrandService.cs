namespace MyShop.Services.BrandService
{
    public class BrandService : IBrandService
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public BrandService(DataContext context, IHttpContextAccessor httpContextAccessor)
        {
            _context = context;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<ServiceResponse<Brand>> CreateBrand(Brand brand)
        {
            if (!_httpContextAccessor.HttpContext.User.IsInRole(Enum.GetName(typeof(Role), Role.Admin)))
                return new ServiceResponse<Brand> { Success = false, Message = "You are not allow to do this action" };

            _context.Brands.Add(brand);
            await _context.SaveChangesAsync();
            return new ServiceResponse<Brand> { Data = brand };
        }

        public async Task<ServiceResponse<bool>> DeleteBrand(int brandId)
        {
            var dbBrand = await _context.Brands.FindAsync(brandId);
            if (dbBrand == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "Brand not found."
                };
            }
            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<List<Brand>>> GetBrands()
        {
            var brands = await _context.Brands.ToListAsync();
            return new ServiceResponse<List<Brand>>
            {
                Data = brands
            };
        }

        public async Task<ServiceResponse<Brand>> UpdateBrand(Brand brand)
        {
            var dbBrand = await _context.Brands
                .FirstOrDefaultAsync(p => p.Id == brand.Id);

            if (dbBrand == null)
            {
                return new ServiceResponse<Brand>
                {
                    Success = false,
                    Message = "Brand not found."
                };
            }

            dbBrand.Name = brand.Name;
            dbBrand.Url = brand.Url;

            await _context.SaveChangesAsync();
            return new ServiceResponse<Brand> { Data = brand };
        }
    }
}
