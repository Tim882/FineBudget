using Data.Service;
using DTOs;
using Models.DbModels.MainModels;

namespace FineBudget.Services.Interfaces
{
    public interface IAccountDataService: IBaseCrudDataService<Account, Guid, AccountRequestDto, AccountResponseDto>
    {
    }
}
