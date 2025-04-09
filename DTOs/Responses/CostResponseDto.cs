using FineBudget.Models;

namespace FineBudget.DTO
{
	public class CostResponseDto : OperationResponseDto
    {
        public bool Required { get; set; }
        public CostCategory CostCategory { get; set; }
    }
}

