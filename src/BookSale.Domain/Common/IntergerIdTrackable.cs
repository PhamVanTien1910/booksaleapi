
namespace dotnet_boilerplate.Domain.Common
{
    public abstract class IntergerIdTrackable : ITrackable
    {
        public int Id { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;
    }
}
