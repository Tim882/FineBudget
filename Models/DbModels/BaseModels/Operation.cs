﻿using System;
using Models.DbModels.MainModels;

namespace Models.DbModels.BaseModels
{
	public class Operation: IBaseEntity
	{
		public long Id { get; set; }
		public DateTime Date { get; set; }
		public string Title { get; set; }
		public string Description { get; set; }
		public decimal Value { get; set; }
		public string TransactionNumber { get; set; } = string.Empty;

		public long AccountId { get; set; }
		public Account Account { get; set; }

		public long? BalanceItemId { get; set; }
		public BalanceItem? BalanceItem { get; set; }
	}
}

