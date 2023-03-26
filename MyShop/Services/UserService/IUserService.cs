namespace MyShop.Services.UserService
{
    public interface IUserService
    {
        // Read
        Task<ServiceResponse<List<User>>> GetUsers();
        Task<ServiceResponse<User>> GetUserInfo(int userId);

        // Update
        Task<ServiceResponse<User>> UpdateUser(User user);
        Task<ServiceResponse<User>> SetVerifiedEmail(string email, string password);
        Task<ServiceResponse<User>> ChangeRole(int userId, Role role);

        // Delete
        Task<ServiceResponse<bool>> DeleteUser(int userId);
    }
}
