using System;
using Models.DbModels.Enums;

namespace Models.DbModels.MainModels
{
	public class Asset: BalanceItem
	{
		public AssetType AssetType { get; set; }
	}
}

