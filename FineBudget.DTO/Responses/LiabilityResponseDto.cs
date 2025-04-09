using System;
using DTOs.BaseDto;
using DTOs.BaseDto.BalanceItem;
using Models.DbModels.Enums;

namespace DTOs.Requests
{
	public class LiabilityResponseDto : BalanceItemResponseDto
    {
        public LiabilityType LiabilityType { get; set; }
    }
}

