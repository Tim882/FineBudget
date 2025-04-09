using System;
using System.ComponentModel.DataAnnotations;

namespace FineBudget.Models
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

