using SmartPass.Repository.Models.Entities;

namespace SmartPass.Services.Models.DTOs.Users
{
    public class UserWithCardsDto : UserDto
    {
        public UserWithCardsDto(User user) : base(user)
        {
            AccessCards = user.AccessCards;
        }

        public ICollection<AccessCard> AccessCards { get; set; }
    }
}
