using DTOs.Requests;
using Models.DbModels.MainModels;

namespace FineBudget.Services.Interfaces
{
    public interface ILiabilityDataService
    {
        public Task<List<LiabilityResponseDto>> GetAllAsync();
        public Task<LiabilityResponseDto> GetByIdAsync(Guid id);
        public Task<LiabilityResponseDto> CreateAsync(LiabilityRequestDto dto);
        public Task<LiabilityResponseDto> UpdateAsync(Guid id, LiabilityRequestDto dto);
        public Task<bool> DeleteAsync(Guid id);
    }
}
