using dotnet_boilerplate.Domain.Common;

namespace dotnet_boilerplate.Domain.Entities
{
    public class Role : IntergerIdTrackable
    {
        public string Name { get; set; } = null!;
        public ICollection<User> Users { get; set; } = [];
    }
}
