using Base.Database;
using FineBudget.DTO;
using FineBudget.Models;

namespace FineBudget.Data
{
    public interface IIncomeDataService : IBaseCrudDataService<Income, Guid, IncomeRequestDto, IncomeResponseDto>
    {
    }
}
