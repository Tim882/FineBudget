using FineBudget.Models;

namespace FineBudget.DTO
{
	public class CostRequestDto: OperationRequestDto
	{
        public bool Required { get; set; }
        public CostCategory CostCategory { get; set; }
    }
}

