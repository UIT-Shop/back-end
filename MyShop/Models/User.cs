using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations.Schema;
using System.Runtime.Serialization;
using System.Text.Json.Serialization;

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
        [IgnoreDataMember]
        [JsonIgnore]
        public byte[] PasswordHash { get; set; }
        [IgnoreDataMember]
        [JsonIgnore]
        public byte[] PasswordSalt { get; set; }
        public Address? Address { get; set; }
        public string Phone { get; set; } = String.Empty;
        public Role Role { get; set; } = Role.Customer;

        public bool Deleted { get; set; } = false;
    }
}
