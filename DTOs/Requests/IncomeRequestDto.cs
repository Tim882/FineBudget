using System;
using DTOs.BaseDto.Operation;
using Models.DbModels.Enums;

namespace DTOs.Requests
{
	public class IncomeRequestDto: OperationRequestDto
	{
        public IncomeCategory IncomeCategory { get; set; }
    }
}

