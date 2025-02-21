using AutoMapper;
using DTOs.Requests;
using FineBudget.Services.Interfaces;
using FineBudget.UnitOfWork;

namespace FineBudget.Services.Implementations
{
    public class IncomeDataService: IIncomeDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public IncomeDataService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<bool> CreateAsync(IncomeRequestDto account)
        {
            throw new NotImplementedException();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<IncomeResponseDto> GetByIdAsync(Guid id)
        {
            throw new NotImplementedException();
        }

        public async Task<List<IncomeResponseDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<bool> UpdateAsync(Guid id, IncomeRequestDto account)
        {
            throw new NotImplementedException();
        }
    }
}
