using Base.Models;
using FineBudget.Models;

namespace FineBudget.Models
{
    public class Asset : BalanceItem
    {
        public AssetType AssetType { get; set; }

        public Guid UserId { get; set; }
        public BudgetUser User { get; set; }
    }
}

