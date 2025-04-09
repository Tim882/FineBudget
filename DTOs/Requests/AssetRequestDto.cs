using FineBudget.Models;

namespace FineBudget.DTO
{
	public class AssetRequestDto: BalanceItemRequestDto
	{
        public AssetType AssetType { get; set; }
    }
}

