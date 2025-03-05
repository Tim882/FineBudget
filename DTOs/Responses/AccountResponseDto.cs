using DTOs.Requests;

namespace DTOs;

public class AccountResponseDto
{
    public Guid Id { get; set; }
    public DateTime Date { get; set; }
    public string Title { get; set; }
    public decimal Balance { get; set; }
    public List<CostResponseDto> Costs { get; set; }
    public List<IncomeResponseDto> Incomes { get; set; }
}

