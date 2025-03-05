using System;
using Models.DbModels.Enums;

namespace DTOs.Requests
{
	public class CostResponseDto
    {
        public Guid Id { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public decimal Value { get; set; }
        public string TransactionNumber { get; set; } = string.Empty;
        public bool Required { get; set; }
        public CostCategory CostCategory { get; set; }
    }
}

