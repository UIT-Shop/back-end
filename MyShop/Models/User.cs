using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;

namespace MyShop.Models
{
    public enum Role
    {
        Customer,
        Admin
    }

    public class User
    {
        public int Id { get; set; }
        [Column(TypeName = "nvarchar(MAX)")]
        public string Name { get; set; } = String.Empty;
        public string Email { get; set; } = String.Empty;
        public byte[] PasswordHash { get; set; }
        public byte[] PasswordSalt { get; set; }
        public Address? Address { get; set; }
        public string phone { get; set; } = String.Empty;
        public Role Role { get; set; } = Role.Customer;
    }
}
