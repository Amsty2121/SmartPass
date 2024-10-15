namespace SmartPass.Repository.Models.EntityInterfaces
{
    public interface ISoftDeleteable
    {
        bool IsDeleted { get; }
        DateTime? DeletedUtcDate { get; }
    }
}
