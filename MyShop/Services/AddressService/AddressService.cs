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
            address.Ward = await _context.Wards.FindAsync(address.WardId);
            if (dbAddress == null)
            {
                //address.UserId = _authService.GetUserId();
                _context.Addresses.Add(address);
            }

            await _context.SaveChangesAsync();
            response.Data = address;
            return response;
        }

        public async Task<ServiceResponse<Address>> GetAddress()
        {
            int userId = _authService.GetUserId();
            //var address = await _context.Addresses
            //    .FirstOrDefaultAsync(a => a.UserId == userId);
            var user = await _context.Users.Include(u => u.Address).Where(u => u.Id == userId).FirstOrDefaultAsync();
            return new ServiceResponse<Address> { Data = user.Address };
        }

        public async Task<ServiceResponse<List<District>>> GetDistricts(int provinceId)
        {
            var address = await _context.Districts
                .Where(p => p.ProvinceId == provinceId).OrderBy(p => p.Name)
                .ToListAsync();
            return new ServiceResponse<List<District>> { Data = address };
        }

        public async Task<ServiceResponse<List<Province>>> GetProvinces()
        {
            var address = await _context.Provinces.OrderBy(p => p.Name).ToListAsync();
            return new ServiceResponse<List<Province>> { Data = address };
        }

        public async Task<ServiceResponse<List<Ward>>> GetWards(int districtId, int provinceId)
        {
            var address = await _context.Wards
                .Where(p => p.DistrictId == districtId && p.ProvinceId == provinceId)
                .OrderBy(p => p.Name)
                .ToListAsync();
            return new ServiceResponse<List<Ward>> { Data = address };
        }
    }
}
