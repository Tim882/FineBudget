using DTOs.Requests;
using Models.DbModels.MainModels;

namespace FineBudget.Services.Interfaces
{
    public interface IIncomeDataService
    {
        public Task<List<IncomeResponseDto>> GetAllAsync();
        public Task<IncomeResponseDto> GetByIdAsync(Guid id);
        public Task<IncomeResponseDto> CreateAsync(IncomeRequestDto dto);
        public Task<IncomeResponseDto> UpdateAsync(Guid id, IncomeRequestDto dto);
        public Task<bool> DeleteAsync(Guid id);
    }
}
