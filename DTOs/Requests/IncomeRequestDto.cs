using FineBudget.Models;

namespace FineBudget.DTO
{
	public class IncomeRequestDto: OperationRequestDto
	{
        public IncomeCategory IncomeCategory { get; set; }
    }
}

