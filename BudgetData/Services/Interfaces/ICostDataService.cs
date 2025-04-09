using Base.Database;
using FineBudget.DTO;
using FineBudget.Models;

namespace FineBudget.Data
{
    public interface ICostDataService : IBaseCrudDataService<Cost, Guid, CostRequestDto, CostResponseDto>
    {
    }
}
