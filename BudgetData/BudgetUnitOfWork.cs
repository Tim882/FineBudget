using Base.Database;

namespace FineBudget.Data
{
    public class BudgetUnitOfWork : UnitOfWork<BudgetContext>, IUnitOfWork
    {
        public BudgetUnitOfWork(BudgetContext context) : base(context)
        {
        }
    }
}
