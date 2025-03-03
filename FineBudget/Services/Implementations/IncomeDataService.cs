using AutoMapper;
using DbRepository;
using DTOs.Requests;
using FineBudget.Services.Interfaces;
using FineBudget.UnitOfWork;
using Models.DbModels.MainModels;

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

        public async Task<IncomeResponseDto> CreateAsync(IncomeRequestDto dto)
        {
            var income = _mapper.Map<Income>(dto);

            var result = await _unitOfWork.IncomeRepository.CreateAsync(income);
            await _unitOfWork.SaveAsync();

            var responseDto = _mapper.Map<IncomeResponseDto>(result);

            return responseDto;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _unitOfWork.IncomeRepository.DeleteAsync(id);
            var result = await _unitOfWork.SaveAsync();

            return result > 0;
        }

        public async Task<IncomeResponseDto> GetByIdAsync(Guid id)
        {
            var income = await _unitOfWork.IncomeRepository.GetAsync(id);

            var responseDto = _mapper.Map<IncomeResponseDto>(income);

            return responseDto;
        }

        public async Task<List<IncomeResponseDto>> GetAllAsync()
        {
            var result = await _unitOfWork.IncomeRepository.GetAllAsync(PredicateBuilder.True<Income>());

            var responseDto = new List<IncomeResponseDto>();

            foreach (var item in result)
            {
                var responseItem = _mapper.Map<IncomeResponseDto>(item);

                responseDto.Add(responseItem);
            }

            return responseDto;
        }

        public async Task<IncomeResponseDto> UpdateAsync(Guid id, IncomeRequestDto dto)
        {
            var income = _mapper.Map<Income>(dto);
            income.Id = id;

            var result = await _unitOfWork.IncomeRepository.Update(income);
            await _unitOfWork.SaveAsync();

            var responseDto = _mapper.Map<IncomeResponseDto>(result);

            return responseDto;
        }
    }
}
