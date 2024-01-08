namespace QueryFilter.Abstractions
{
    public interface ITenantRelated
    {
        Guid TenantId { get; set; }
    }
}
