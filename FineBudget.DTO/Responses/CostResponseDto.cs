using System;
using DTOs.BaseDto;
using DTOs.BaseDto.Operation;
using Models.DbModels.Enums;

namespace DTOs.Requests
{
	public class CostResponseDto : OperationResponseDto
    {
        public bool Required { get; set; }
        public CostCategory CostCategory { get; set; }
    }
}

