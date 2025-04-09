using DTOs.BaseDto;

namespace DTOs;

public class AccountRequestDto: BaseRequestDto
{
    public DateTime Date { get; set; }
    public string Title { get; set; }
    public decimal Balance { get; set; }
}

