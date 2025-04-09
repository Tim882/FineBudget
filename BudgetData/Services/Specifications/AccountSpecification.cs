using Base.Database;
using Base.Models;
using FineBudget.Models;
using System.Linq.Expressions;

namespace FineBudget.Data
{
    public class AccountSpecification: BaseSpecification<Account>
    {
        public AccountSpecification(Expression<Func<Account, bool>> criteria) : base(criteria)
        {
            AddInclude(a => a.Costs);
            AddInclude(a => a.Incomes);
        }

        public AccountSpecification(QueryParameters parameters) : base(parameters)
        {
            AddInclude(a => a.Costs);
            AddInclude(a => a.Incomes);
        }
    }
}
