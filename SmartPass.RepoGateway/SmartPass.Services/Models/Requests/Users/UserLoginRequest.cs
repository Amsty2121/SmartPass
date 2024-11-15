using System.ComponentModel.DataAnnotations;

namespace SmartPass.Services.Models.Requests.Users
{
    public class UserLoginRequest
    {

        [Required]
        [MaxLength(50)]
        public string Login { get; set; }

        [Required]
        [MaxLength(50)]
        [MinLength(8)]
        public string Password { get; set; }
    }
}
