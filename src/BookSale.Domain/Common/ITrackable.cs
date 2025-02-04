namespace dotnet_boilerplate.Domain.Common
{
    public interface ITrackable
    {
        public DateTime CreatedAt { get; set; }
        public DateTime UpdatedAt { get; set; }
    }
}
