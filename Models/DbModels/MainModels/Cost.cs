using System;
using Models.DbModels.BaseModels;
using Models.DbModels.Enums;

namespace Models.DbModels.MainModels
{
	public class Cost: Operation
	{
		public bool Required { get; set; }
		public CostCategory CostCategory { get; set; }
	}
}

