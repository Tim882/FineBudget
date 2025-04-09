using Base.Models;

namespace FineBudget.DTO
{
    public class BalanceItemResponseDto : BaseResponseDto
    {
        public string Title { get; set; }
        public decimal Value { get; set; }
        public DateTime Date { get; set; }
    }
}
