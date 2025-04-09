using Base.Models;

namespace FineBudget.Models
{
    public abstract class Operation : BaseEntity
    {
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string TransactionNumber { get; set; } = string.Empty;

        public Guid AccountId { get; set; }
        public Account Account { get; set; }

        public Guid? AssetId { get; set; }
        public Asset? Asset { get; set; }

        public Guid? LiabilityId { get; set; }
        public Liability? Liability { get; set; }
    }
}

