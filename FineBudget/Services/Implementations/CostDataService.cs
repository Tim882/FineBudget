using AutoMapper;
using DTOs.Requests;
using FineBudget.Services.Interfaces;
using FineBudget.UnitOfWork;
using Models.DbModels.MainModels;

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

        public async Task<CostResponseDto> CreateAsync(CostRequestDto dto)
        {
            Cost cost = _mapper.Map<Cost>(dto);

            var createdCost = await _unitOfWork.CostRepository.CreateAsync(cost);
            await _unitOfWork.SaveAsync();

            CostResponseDto costResponseDto = _mapper.Map<CostResponseDto>(createdCost);

            return costResponseDto;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _unitOfWork.CostRepository.DeleteAsync(id);
            var result = await _unitOfWork.SaveAsync();

            return result > 0;
        }

        public async Task<CostResponseDto> GetByIdAsync(Guid id)
        {
            Cost result = await _unitOfWork.CostRepository.GetAsync(id);

            CostResponseDto response = _mapper.Map<CostResponseDto>(result);

            return response;
        }

        public async Task<List<CostResponseDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<CostResponseDto> UpdateAsync(Guid id, CostRequestDto dto)
        {
            Cost cost = await _unitOfWork.CostRepository.GetAsync(id);

            _mapper.Map(dto, cost);

            var result = await _unitOfWork.CostRepository.Update(cost);
            await _unitOfWork.SaveAsync();

            var responseDto = _mapper.Map<CostResponseDto>(result);

            return responseDto;
        }
    }
}
