using Data.Service;
using DTOs.Requests;
using Models.DbModels.MainModels;

namespace FineBudget.Services.Interfaces
{
    public interface IIncomeDataService : IBaseCrudDataService<Income, Guid, IncomeRequestDto, IncomeResponseDto>
    {
    }
}
