using System;
using System.ComponentModel.DataAnnotations;

namespace Models.DbModels.Enums
{
	public enum LiabilityType
	{
		[Display(Name = "Залоговые кредиты")]
		CollaterralLoans,
		[Display(Name = "Беззалоговые обязательства")]
		UnsecuredLoans
	}
}

