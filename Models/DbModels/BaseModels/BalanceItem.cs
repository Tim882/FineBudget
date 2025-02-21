using Models.DbModels.BaseModels;
using System;
namespace Models.DbModels
{
	public abstract class BalanceItem: BaseEntity
    {
		public DateTime Date { get; set; }
		public string Title { get; set; }
		public decimal Value { get; set; }
	}
}

