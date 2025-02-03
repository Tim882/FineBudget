using System;
using System.ComponentModel.DataAnnotations;

namespace Models.DbModels.Enums
{
	public enum AssetType
	{
		[Display(Name = "Денежные средства")]
		Cash,
		[Display(Name = "Инвестиции")]
		Investment,
		[Display(Name = "Недвижимость")]
		RealtyEstate,
		[Display(Name = "Другое")]
		Other
	}
}

