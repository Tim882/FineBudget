using Base.Models;
using FineBudget.Models;

namespace FineBudget.Models
{
    public class Asset : BalanceItem
    {
        public AssetType AssetType { get; set; }
    }
}

