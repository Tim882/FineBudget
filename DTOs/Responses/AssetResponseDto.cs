using System;
using Models.DbModels.Enums;

namespace DTOs.Responses
{
	public class AssetResponseDto
    {
        public Guid Id { get; set; }
        public AssetType AssetType { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public decimal Value { get; set; }
    }
}

