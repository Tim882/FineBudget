using Base.Models;

namespace FineBudget.DTO;

public class AccountRequestDto: BaseRequestDto
{
    public DateTime Date { get; set; }
    public string Title { get; set; }
    public decimal Balance { get; set; }
}

