using DTOs.Requests;
using Models.DbModels.MainModels;

namespace FineBudget.Services.Interfaces
{
    public interface ICostDataService
    {
        public Task<List<CostResponseDto>> GetAllAsync();
        public Task<CostResponseDto> GetByIdAsync(Guid id);
        public Task<CostResponseDto> CreateAsync(CostRequestDto account);
        public Task<CostResponseDto> UpdateAsync(Guid id, CostRequestDto account);
        public Task<bool> DeleteAsync(Guid id);
    }
}
