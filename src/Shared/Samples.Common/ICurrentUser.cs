namespace Samples.Common
{
    public interface ICurrentUser
    {
        Guid UserId { get; }
        Guid TenantId { get; }
    }
}
