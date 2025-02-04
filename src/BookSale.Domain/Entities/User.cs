using dotnet_boilerplate.Domain.Common;
using BookSale.Domain.Entities;
using System.Data;

namespace dotnet_boilerplate.Domain.Entities
{
    public class User : IntergerIdTrackable
    {
        public string FullName { get; set; }
        public string Email { get; set; } = null!;
        public string Password { get; set; } = null!;

        public DateTime? LastLogin { get; set; }
        public bool IsSuperUser { get; set; }
        public bool IsStaff { get; set; }
        public bool IsActive { get; set; }
        public bool IsEmailConfirmed { get; set; }
        public DateTime DateJoined { get; set; } = DateTime.UtcNow;
        public int RoleId { get; set; }
        public Role Role { get; set; } = null!;
        public ICollection<Review> Reviews { get; set; }
        public ICollection<Cart> Carts { get; set; }

    }
}
