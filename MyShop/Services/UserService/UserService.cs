using Firebase.Auth;

namespace MyShop.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;
        private readonly IAddressService _addressService;
        private readonly IAuthService _authService;
        private const string API_KEY = "AIzaSyDRz4mCzJhNWnJh5KriLOQF1zPdBqZu03Y";

        public UserService(DataContext context, IAddressService addressService, IAuthService authService)
        {
            _context = context;
            _addressService = addressService;
            _authService = authService;
        }
        public async Task<ServiceResponse<List<MyShop.Models.User>>> GetUsers()
        {
            var users = await _context.Users.Where(u => !u.Deleted).ToListAsync();
            return new ServiceResponse<List<MyShop.Models.User>> { Data = users };
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

        public async Task<ServiceResponse<MyShop.Models.User>> GetUserInfo(int userId)
        {
            var dbUser = await _context.Users.Include(u => u.Address).ThenInclude(ad => ad.Ward)
                            .ThenInclude(w => w.District).ThenInclude(d => d.Province)
                            .FirstOrDefaultAsync(u => u.Id == userId);
            return dbUser == null
                ? new ServiceResponse<MyShop.Models.User>
                {
                    Success = false,
                    Data = null,
                    Message = "User not found."
                }
                : new ServiceResponse<MyShop.Models.User> { Data = dbUser };
        }

        public async Task<ServiceResponse<MyShop.Models.User>> UpdateUser(MyShop.Models.User user)
        {
            user.Id = _authService.GetUserId();
            var dbUser = await _context.Users.FindAsync(user.Id);
            if (dbUser == null)
            {
                return new ServiceResponse<MyShop.Models.User>
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
            dbUser.IsEmailVerified = user.IsEmailVerified;

            await _context.SaveChangesAsync();
            return await GetUserInfo(user.Id);
        }

        public async Task<ServiceResponse<MyShop.Models.User>> ChangeRole(int userId, Role role)
        {
            var dbUser = await _context.Users.FindAsync(userId);
            if (dbUser == null)
            {
                return new ServiceResponse<MyShop.Models.User>
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

        public async Task<ServiceResponse<MyShop.Models.User>> SetVerifiedEmail(string email, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => x.Email.ToLower().Equals(email.ToLower()));
            if (user == null)
            {
                return new ServiceResponse<MyShop.Models.User>
                {
                    Success = false,
                    Data = null,
                    Message = "User not found."
                };
            }

            FirebaseAuthProvider firebaseAuthProvider = new FirebaseAuthProvider(new FirebaseConfig(API_KEY));

            var userCredential = await firebaseAuthProvider.SignInWithEmailAndPasswordAsync(email, password);
            user.IsEmailVerified = userCredential.User.IsEmailVerified;

            await _context.SaveChangesAsync();
            return await GetUserInfo(user.Id);
        }
    }
}
