using Base.Models;

namespace FineBudget.Models
{
    public class BudgetUser: ApplicationUser
    {
        public List<Account> Accounts { get; set; }
        public List<Asset> Assets { get; set; }
        public List<Liability> Liabilities { get; set; }
    }
}
