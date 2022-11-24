namespace MyShop.Services.UserService
{
    public interface IUserService
    {
        // Read
        Task<ServiceResponse<List<User>>> GetUsers();
        Task<ServiceResponse<User>> GetUserInfo(int userId);

        // Update
        Task<ServiceResponse<User>> UpdateUser(User user);

        // Delete
        Task<ServiceResponse<bool>> DeleteUser(int userId);
    }
}
