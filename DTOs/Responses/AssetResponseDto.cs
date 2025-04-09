using FineBudget.Models;

namespace FineBudget.DTO
{
	public class AssetResponseDto : BalanceItemResponseDto
    {
        public AssetType AssetType { get; set; }
    }
}

