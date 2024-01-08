namespace QueryFilter.Abstractions
{
    public interface ICurrentUser
    {
        Guid UserId { get; }
        Guid TenantId { get; }
    }
}
