using AutoMapper;
using DTOs.Requests;
using DTOs.Responses;
using FineBudget.Services.Interfaces;
using FineBudget.UnitOfWork;
using Models.DbModels.MainModels;

namespace FineBudget.Services.Implementations
{
    public class AssetDataService: IAssetDataService
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IMapper _mapper;

        public AssetDataService(IUnitOfWork unitOfWork, IMapper mapper)
        {
            _unitOfWork = unitOfWork;
            _mapper = mapper;
        }

        public async Task<AssetResponseDto> CreateAsync(AssetRequestDto dto)
        {
            throw new NotImplementedException();

            Asset asset = _mapper.Map<Asset>(dto);

            await _unitOfWork.AssetRepository.CreateAsync(asset);
            await _unitOfWork.SaveAsync();
        }

        public async Task<bool> DeleteAsync(Guid id)
        {
            await _unitOfWork.AssetRepository.DeleteAsync(id);

            return await _unitOfWork.SaveAsync() > 0;
        }

        public async Task<AssetResponseDto> GetByIdAsync(Guid id)
        {
            var result = _unitOfWork.AssetRepository.GetAsync(id);

            AssetResponseDto response = _mapper.Map<AssetResponseDto>(result);

            return response;
        }

        public async Task<List<AssetResponseDto>> GetAllAsync()
        {
            throw new NotImplementedException();
        }

        public async Task<AssetResponseDto> UpdateAsync(Guid id, AssetRequestDto dto)
        {
            Asset asset = await _unitOfWork.AssetRepository.GetAsync(id);
            _mapper.Map(dto, asset);

            var newAsset = await _unitOfWork.AssetRepository.Update(asset);
            await _unitOfWork.SaveAsync();

            var responseDto = _mapper.Map<AssetResponseDto>(newAsset);

            return responseDto;
        }
    }
}
