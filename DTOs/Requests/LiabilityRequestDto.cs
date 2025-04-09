using FineBudget.Models;

namespace FineBudget.DTO
{
	public class LiabilityRequestDto: BalanceItemRequestDto
	{
        public LiabilityType LiabilityType { get; set; }
    }
}

