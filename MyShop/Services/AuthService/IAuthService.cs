namespace MyShop.Services.AuthService
{
    public interface IAuthService
    {
        Task<ServiceResponse<int>> Register(MyShop.Models.User user, string password);
        Task<bool> UserExists(string email);
        Task<ServiceResponse<string>> Login(string email, string password);
        Task<ServiceResponse<bool>> ChangePassword(int userId, string oldPassword, string newPassword);
        Task<ServiceResponse<string>> CheckAuthen();
        int GetUserId();
        string GetUserEmail();
    }
}
