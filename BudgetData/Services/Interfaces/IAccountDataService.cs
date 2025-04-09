using Base.Database;
using FineBudget.DTO;
using FineBudget.Models;

namespace FineBudget.Data
{
    public interface IAccountDataService: IBaseCrudDataService<Account, Guid, AccountRequestDto, AccountResponseDto>
    {
    }
}
