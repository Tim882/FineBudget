using Data.Service;
using DTOs;
using DTOs.Requests;
using DTOs.Responses;
using Models.DbModels.MainModels;

namespace FineBudget.Services.Interfaces
{
    public interface IAssetDataService : IBaseCrudDataService<Asset, Guid, AssetRequestDto, AssetResponseDto>
    {
    }
}
