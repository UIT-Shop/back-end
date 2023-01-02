namespace MyShop.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IAddressService _addressService;
        private readonly IAuthService _authService;

        public UserService(DataContext context, IAddressService addressService, IAuthService authService)
        {
            _context = context;
            _addressService = addressService;
            _authService = authService;
        }
        public async Task<ServiceResponse<List<User>>> GetUsers()
        {
            var users = await _context.Users.Where(u => !u.Deleted).ToListAsync();
            return new ServiceResponse<List<User>> { Data = users };
        }
        public async Task<ServiceResponse<bool>> DeleteUser(int userId)
        {
            var dbUser = await _context.Users.FindAsync(userId);
            if (dbUser == null)
            {
                return new ServiceResponse<bool>
                {
                    Success = false,
                    Data = false,
                    Message = "User not found."
                };
            }

            dbUser.Deleted = true;

            await _context.SaveChangesAsync();
            return new ServiceResponse<bool> { Data = true };
        }

        public async Task<ServiceResponse<User>> GetUserInfo(int userId)
        {
            var dbUser = await _context.Users.Include(u => u.Address).ThenInclude(ad => ad.Ward)
                            .ThenInclude(w => w.District).ThenInclude(d => d.Province)
                            .FirstOrDefaultAsync(u => u.Id == userId);
            return dbUser == null
                ? new ServiceResponse<User>
                {
                    Success = false,
                    Data = null,
                    Message = "User not found."
                }
                : new ServiceResponse<User> { Data = dbUser };
        }

        public async Task<ServiceResponse<User>> UpdateUser(User user)
        {
            user.Id = _authService.GetUserId();
            var dbUser = await _context.Users.FindAsync(user.Id);
            if (dbUser == null)
            {
                return new ServiceResponse<User>
                {
                    Success = false,
                    Data = null,
                    Message = "User not found."
                };
            }

            var dbAddress = (await _addressService.AddAddress(user.Address)).Data;

            dbUser.Name = user.Name;
            dbUser.Phone = user.Phone;
            dbUser.Role = user.Role;
            dbUser.Address = dbAddress;
            dbUser.AddressId = dbAddress?.Id;

            await _context.SaveChangesAsync();
            return await GetUserInfo(user.Id);
        }

        public async Task<ServiceResponse<User>> ChangeRole(int userId, Role role)
        {
            var dbUser = await _context.Users.FindAsync(userId);
            if (dbUser == null)
            {
                return new ServiceResponse<User>
                {
                    Success = false,
                    Data = null,
                    Message = "User not found."
                };
            }

            dbUser.Role = role;

            await _context.SaveChangesAsync();
            return await GetUserInfo(userId);
        }
    }
}
