using AutoMapper;
using DTOs.Requests;
using FineBudget.Services.Interfaces;
using FineBudget.UnitOfWork;

namespace FineBudget.Services.Implementations
{
    public class CostDataService: ICostDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public CostDataService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(CostRequestDto account)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<CostResponseDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<CostResponseDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Guid id, CostRequestDto account)
        {
            throw new NotImplementedException();
        }
    }
}
