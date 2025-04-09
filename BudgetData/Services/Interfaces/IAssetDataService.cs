using Base.Database;
using FineBudget.DTO;
using FineBudget.Models;

namespace FineBudget.Data
{
    public interface IAssetDataService : IBaseCrudDataService<Asset, Guid, AssetRequestDto, AssetResponseDto>
    {
    }
}
