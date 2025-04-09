using Base.Models;

namespace FineBudget.DTO
{
    public class OperationResponseDto : BaseResponseDto
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string TransactionNumber { get; set; } = string.Empty;

        public Guid AccountId { get; set; }

        public Guid? AssetId { get; set; }

        public Guid? LiabilityId { get; set; }
    }
}
