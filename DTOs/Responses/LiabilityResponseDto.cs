using System;
using Models.DbModels.Enums;

namespace DTOs.Requests
{
	public class LiabilityResponseDto
    {
        public Guid Id { get; set; }
        public LiabilityType LiabilityType { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public decimal Value { get; set; }
    }
}

