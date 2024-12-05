using SmartPass.Repository.Models.Entities;
using SmartPass.Repository.Models.Enums;
using SmartPass.Services.Models.DTOs.AccessCards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace SmartPass.Services.Models.Resposes.AccessCards
{
    public class GetMyAccessCardsMobileResponse
    {
        public GetMyAccessCardsMobileResponse(IEnumerable<AccessCardDto> accessCards, User user)
        {
            AccessCards = accessCards;
            UserInfo = new UserOwner(user.Id, user.UserName, user.Department);
        }
        
        public IEnumerable<AccessCardDto> AccessCards { get; set; }
        public UserOwner UserInfo {  get; set; }
    }

    public record UserOwner(Guid UserId, string UserName, string Department) { }



}
