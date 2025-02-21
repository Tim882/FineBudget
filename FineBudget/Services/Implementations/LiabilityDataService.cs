using AutoMapper;
using DTOs.Requests;
using FineBudget.Services.Interfaces;
using FineBudget.UnitOfWork;

namespace FineBudget.Services.Implementations
{
    public class LiabilityDataService: ILiabilityDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public LiabilityDataService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(LiabilityRequestDto account)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<LiabilityResponseDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<LiabilityResponseDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Guid id, LiabilityRequestDto account)
        {
            throw new NotImplementedException();
        }
    }
}
