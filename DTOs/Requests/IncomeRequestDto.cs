using System;
using Models.DbModels.Enums;

namespace DTOs.Requests
{
	public class IncomeRequestDto
	{
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string TransactionNumber { get; set; } = string.Empty;
        public IncomeCategory IncomeCategory { get; set; }
        public Guid AccountId { get; set; }
    }
}

