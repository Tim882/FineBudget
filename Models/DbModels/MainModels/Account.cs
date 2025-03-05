using System;
using Models.DbModels.BaseModels;

namespace Models.DbModels.MainModels
{
	public class Account: BaseEntity
    {
		public string Title { get; set; }
		public decimal Balance { get; set; }

		public List<Income> Incomes { get; set; }
		public List<Cost> Costs { get; set; }
	}
}

