using AutoMapper;
using Base.Database;
using FineBudget.DTO;
using FineBudget.Models;
using FluentValidation;

namespace FineBudget.Data
{
    public class AssetDataService : BaseCrudDataService<Asset, Guid, AssetRequestDto, AssetResponseDto>, IAssetDataService
    {
        public AssetDataService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<AssetRequestDto> validator)
            : base(unitOfWork, mapper, validator) { }
    }
}
