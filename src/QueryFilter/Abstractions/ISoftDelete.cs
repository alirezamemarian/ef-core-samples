namespace QueryFilter.Abstractions
{
    public interface ISoftDelete
    {
        bool IsDeleted { get; set; }
    }
}
