using System;
using DTOs.BaseDto.BalanceItem;
using Models.DbModels.Enums;

namespace DTOs.Requests
{
	public class LiabilityRequestDto: BalanceItemRequestDto
	{
        public LiabilityType LiabilityType { get; set; }
    }
}

