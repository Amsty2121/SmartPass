﻿using LanguageExt;
using LanguageExt.Common;
using SmartPass.Services.Models.DTOs.AccessCards;
using SmartPass.Services.Models.Requests.AccessCards;
using SmartPass.Services.Models.Resposes.AccessCards;

namespace SmartPass.Services.Interfaces
{
    public interface IAccessCardService
    {
        Task<Option<AccessCardDto>> Get(Guid id, CancellationToken ct = default);
        Task<IEnumerable<AccessCardDto>> GetAll(CancellationToken ct = default);
        Task<IEnumerable<AccessCardToMobileDto>> GetAllByUserId(Guid userId, CancellationToken ct = default);
        Task<Option<GetMyAccessCardsMobileResponse>> GetAllByUserIdMobile(Guid userId, CancellationToken ct = default);


        Task<Result<AddAccessCardResponse>> Create(AddAccessCardRequest request, CancellationToken ct = default);
        Task<Result<AccessCardDto>> Update(UpdateAccessCardDto updateDto, CancellationToken ct = default);
        Task<Result<AccessCardDto>> Delete(Guid id, CancellationToken ct = default);
        Task<Result<AccessCardDto>> DeleteSoft(Guid id, CancellationToken ct = default);

        //Task<Option<AccessCardWithSessionsDto>> GetAccessCardWithSessions(CancellationToken ct = default);
        //Task<IEnumerable<AccessCardWithDeletedFlagDto>> GetAccessCardsWithDeletedFlag(CancellationToken ct = default);
    }
}
