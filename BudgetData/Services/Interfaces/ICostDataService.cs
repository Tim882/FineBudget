using Data.Service;
using DTOs.Requests;
using DTOs.Responses;
using Models.DbModels.MainModels;

namespace FineBudget.Services.Interfaces
{
    public interface ICostDataService : IBaseCrudDataService<Cost, Guid, CostRequestDto, CostResponseDto>
    {
    }
}
