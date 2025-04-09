using System;
using System.ComponentModel.DataAnnotations;

namespace FineBudget.Models
{
    public enum LiabilityType
    {
        [Display(Name = "Залоговые кредиты")]
        CollaterralLoans,
        [Display(Name = "Беззалоговые обязательства")]
        UnsecuredLoans
    }
}

