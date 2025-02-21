using DTOs;
using Models.DbModels.MainModels;

namespace FineBudget.Services.Interfaces
{
    public interface IAccountDataService
    {
        public Task<List<AccountResponseDto>> GetAllAsync();
        public Task<AccountResponseDto> GetByIdAsync(Guid id);
        public Task<AccountResponseDto> CreateAsync(AccountRequestDto account);
        public Task<AccountResponseDto> UpdateAsync(Guid id, AccountRequestDto account);
        public Task<bool> DeleteAsync(Guid id);
    }
}
