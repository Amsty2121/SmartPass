using SmartPass.Repository.Models.Entities;

namespace SmartPass.Services.Models.DTOs.Users
{
    public class UserWithDeletedFlagDto : UserDto
    {
        public UserWithDeletedFlagDto(User user) : base(user)
        {
            IsDeleted = user.IsDeleted;
            DeletedUtcDate = user.DeletedUtcDate;
        }

        public bool IsDeleted { get; set; }
        public DateTime? DeletedUtcDate { get; set; }
    }
}
