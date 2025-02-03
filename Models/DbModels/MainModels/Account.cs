using System;
using Models.DbModels.BaseModels;

namespace Models.DbModels.MainModels
{
	public class Account: IBaseEntity
	{
        public long Id { get; set; }
        public DateTime Date { get; set; }
		public string Title { get; set; }
		public decimal Balance { get; set; }

		public List<Operation> Operations { get; set; }
	}
}

