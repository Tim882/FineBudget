using Base.Database;
using FineBudget.Models;
using FineBudget.DTO;


namespace FineBudget.Data
{
    public interface ILiabilityDataService : IBaseCrudDataService<Liability, Guid, LiabilityRequestDto, LiabilityResponseDto>
    {
    }
}
