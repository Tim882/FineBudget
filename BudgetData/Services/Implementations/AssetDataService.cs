using AutoMapper;
using Data.Service;
using Data.UnitOfWork;
using DTOs.Requests;
using DTOs.Responses;
using FineBudget.Services.Interfaces;
using FluentValidation;
using Models.DbModels.MainModels;

namespace FineBudget.Services.Implementations
{
    public class AssetDataService : BaseCrudDataService<Asset, Guid, AssetRequestDto, AssetResponseDto>, IAssetDataService
    {
        public AssetDataService(IUnitOfWork unitOfWork, IMapper mapper, IValidator<AssetRequestDto> validator)
            : base(unitOfWork, mapper, validator) { }
    }
}
