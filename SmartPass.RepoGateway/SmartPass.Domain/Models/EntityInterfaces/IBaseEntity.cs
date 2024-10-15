using System.ComponentModel.DataAnnotations;
namespace SmartPass.Repository.Models.EntityInterfaces
{
    public interface IBaseEntity : ISoftDeleteable
    {
        Guid Id { get; }

    }
}
