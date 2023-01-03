using System.ComponentModel.DataAnnotations;

namespace MyShop.Models
{
    public class UserChangePassword
    {
        [Required, StringLength(100, MinimumLength = 6)]
        public string OldPassword { get; set; } = string.Empty;
        [Required, StringLength(100, MinimumLength = 6)]
        public string NewPassword { get; set; } = string.Empty;
        [Compare("NewPassword", ErrorMessage = "The passwords do not match.")]
        public string ConfirmPassword { get; set; } = string.Empty;
    }
}
