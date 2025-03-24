using Data.Service;
using DTOs.Requests;
using Models.DbModels.MainModels;

namespace FineBudget.Services.Interfaces
{
    public interface ILiabilityDataService : IBaseCrudDataService<Liability, Guid, LiabilityRequestDto, LiabilityResponseDto>
    {
    }
}
