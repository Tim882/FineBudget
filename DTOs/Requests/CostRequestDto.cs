using System;
using Models.DbModels.Enums;

namespace DTOs.Requests
{
	public class CostRequestDto
	{
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string TransactionNumber { get; set; } = string.Empty;
        public bool Required { get; set; }
        public CostCategory CostCategory { get; set; }
        public Guid AccountId { get; set; }
        public Guid? AssetId { get; set; }
        public Guid? LiabilityId { get; set; }
    }
}

