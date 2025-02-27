using AutoMapper;
using DTOs.Requests;
using FineBudget.Services.Interfaces;
using FineBudget.UnitOfWork;
using Models.DbModels.MainModels;

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

        public async Task<LiabilityResponseDto> CreateAsync(LiabilityRequestDto dto)
        {
            var liability = _mapper.Map<Liability>(dto);

            var result = await _unitOfWork.LiabilityRepository.CreateAsync(liability);
            await _unitOfWork.SaveAsync();

            var responseDto = _mapper.Map<LiabilityResponseDto>(result);

            return responseDto;
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _unitOfWork.LiabilityRepository.DeleteAsync(id);
            var result = await _unitOfWork.SaveAsync();

            return result > 0;
        }

        public async Task<LiabilityResponseDto> GetByIdAsync(Guid id)
        {
            var result = await _unitOfWork.LiabilityRepository.GetAsync(id);

            var responseDto = _mapper?.Map<LiabilityResponseDto>(result);

            return responseDto;
        }

        public async Task<List<LiabilityResponseDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<LiabilityResponseDto> UpdateAsync(Guid id, LiabilityRequestDto dto)
        {
            var liability = _mapper.Map<Liability>(dto);
            liability.Id = id;

            var result = await _unitOfWork.LiabilityRepository.Update(liability);
            await _unitOfWork.SaveAsync();

            var responseDto = _mapper.Map<LiabilityResponseDto>(result);

            return responseDto;
        }
    }
}
