using Base.Models;

namespace FineBudget.DTO;

public class AccountResponseDto : BaseResponseDto
{
    public DateTime Date { get; set; }
    public string Title { get; set; }
    public decimal Balance { get; set; }
    public List<CostResponseDto> Costs { get; set; }
    public List<IncomeResponseDto> Incomes { get; set; }
}

