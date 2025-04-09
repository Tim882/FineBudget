using FineBudget.Models;

namespace FineBudget.DTO
{
	public class LiabilityResponseDto : BalanceItemResponseDto
    {
        public LiabilityType LiabilityType { get; set; }
    }
}

