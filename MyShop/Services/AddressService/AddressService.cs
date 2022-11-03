namespace MyShop.Services.AddressService
{
    public class AddressService : IAddressService
    {
        private readonly DataContext _context;
        private readonly IAuthService _authService;

        public AddressService(DataContext context, IAuthService authService)
        {
            _context = context;
            _authService = authService;
        }

        public async Task<ServiceResponse<Address>> AddOrUpdateAddress(Address address)
        {
            var response = new ServiceResponse<Address>();
            var dbAddress = (await GetAddress()).Data;
            if (dbAddress == null)
            {
                address.UserId = _authService.GetUserId();
                _context.Addresses.Add(address);
                response.Data = address;
            }
            else
            {
                dbAddress.Ward = address.Ward;
                response.Data = dbAddress;
            }

            await _context.SaveChangesAsync();

            return response;
        }

        public async Task<ServiceResponse<Address>> GetAddress()
        {
            int userId = _authService.GetUserId();
            var address = await _context.Addresses
                .FirstOrDefaultAsync(a => a.UserId == userId);
            return new ServiceResponse<Address> { Data = address };
        }

        public async Task<ServiceResponse<List<District>>> GetDistricts(int provinceId)
        {
            var address = await _context.Districts
                .Where(p => p.ProvinceId == provinceId)
                .ToListAsync();
            return new ServiceResponse<List<District>> { Data = address };
        }

        public async Task<ServiceResponse<List<Province>>> GetProvinces()
        {
            var address = await _context.Provinces.ToListAsync();
            return new ServiceResponse<List<Province>> { Data = address };
        }

        public async Task<ServiceResponse<List<Ward>>> GetWards(int districtId, int provinceId)
        {
            var address = await _context.Wards
                .Where(p => p.DistrictId == districtId && p.ProvinceId == provinceId)
                .ToListAsync();
            return new ServiceResponse<List<Ward>> { Data = address };
        }
    }
}
