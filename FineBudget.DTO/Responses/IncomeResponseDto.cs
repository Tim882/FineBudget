using System;
using DTOs.BaseDto;
using DTOs.BaseDto.Operation;
using Models.DbModels.Enums;

namespace DTOs.Requests
{
	public class IncomeResponseDto : OperationResponseDto
    {
        public IncomeCategory IncomeCategory { get; set; }
    }
}

