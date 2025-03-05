using Models.DbModels.BaseModels;
using Models.DbModels.MainModels;
using System;
namespace Models.DbModels
{
	public abstract class BalanceItem: BaseEntity
    {
		public string Title { get; set; }
		public decimal Value { get; set; }

		public List<Income> Incomes { get; set; }
		public List<Cost> Costs { get; set; }
	}
}

