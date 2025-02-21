using DTOs.Requests;
using Models.DbModels.MainModels;

namespace FineBudget.Services.Interfaces
{
    public interface IIncomeDataService
    {
        public Task<List<IncomeResponseDto>> GetAllAsync();
        public Task<IncomeResponseDto> GetByIdAsync(Guid id);
        public Task<IncomeResponseDto> CreateAsync(IncomeRequestDto account);
        public Task<IncomeResponseDto> UpdateAsync(Guid id, IncomeRequestDto account);
        public Task<bool> DeleteAsync(Guid id);
    }
}
