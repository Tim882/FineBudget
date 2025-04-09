using System;
using DTOs.BaseDto;
using DTOs.BaseDto.BalanceItem;
using Models.DbModels.Enums;

namespace DTOs.Responses
{
	public class AssetResponseDto : BalanceItemResponseDto
    {
        public AssetType AssetType { get; set; }
    }
}

