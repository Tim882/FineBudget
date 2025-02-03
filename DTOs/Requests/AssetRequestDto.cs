using System;
using Models.DbModels.Enums;

namespace DTOs.Requests
{
	public class AssetRequestDto
	{
        public AssetType AssetType { get; set; }
        public DateTime Date { get; set; }
        public string Title { get; set; }
        public decimal Value { get; set; }
    }
}

