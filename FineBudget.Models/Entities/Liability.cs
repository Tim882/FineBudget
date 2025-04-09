using FineBudget.Models;

namespace FineBudget.Models
{
    public class Liability : BalanceItem
    {
        public LiabilityType LiabilityType { get; set; }

        public Guid UserId { get; set; }
        public BudgetUser User { get; set; }
    }
}

