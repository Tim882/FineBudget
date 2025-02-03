using System;
using Models.DbModels.Enums;

namespace Models.DbModels.MainModels
{
	public class Liability: BalanceItem
	{
		public LiabilityType LiabilityType { get; set; }
	}
}

