using System;
namespace Models.DbModels
{
	public abstract class BalanceItem: IBaseEntity
	{
		public long Id { get; set; }
		public DateTime Date { get; set; }
		public string Title { get; set; }
		public decimal Value { get; set; }
	}
}

