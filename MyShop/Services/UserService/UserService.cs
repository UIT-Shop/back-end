namespace MyShop.Services.UserService
{
    public class UserService : IUserService
    {
        private readonly DataContext _context;

        public UserService(DataContext context)
        {
            _context = context;
        }
        public async Task<ServiceResponse<List<User>>> GetUsers()
        {
            var users = await _context.Users.ToListAsync();
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

            await _context.SaveChangesAsync();
            return new ServiceResponse<User> { Data = dbUser };
        }

        public async Task<ServiceResponse<User>> UpdateUser(User user)
        {
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

            dbUser.Name = user.Name;
            dbUser.Phone = user.Phone;
            dbUser.Role = user.Role;
            dbUser.Address = user.Address;

            await _context.SaveChangesAsync();
            return await GetUserInfo(user.Id);
        }
    }
}
