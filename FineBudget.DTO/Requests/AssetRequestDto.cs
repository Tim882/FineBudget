using System;
using DTOs.BaseDto.BalanceItem;
using Models.DbModels.Enums;

namespace DTOs.Requests
{
	public class AssetRequestDto: BalanceItemRequestDto
	{
        public AssetType AssetType { get; set; }
    }
}

