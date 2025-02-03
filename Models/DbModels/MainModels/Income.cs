using System;
using Models.DbModels.BaseModels;
using Models.DbModels.Enums;

namespace Models.DbModels.MainModels
{
	public class Income: Operation
	{
		public IncomeCategory IncomeCategory { get; set; }
	}
}

