using System;
using DTOs.BaseDto.Operation;
using Models.DbModels.Enums;

namespace DTOs.Requests
{
	public class CostRequestDto: OperationRequestDto
	{
        public bool Required { get; set; }
        public CostCategory CostCategory { get; set; }
    }
}

