using DbRepository.Specifications;
using FineBudget.Models;
using Models.DbModels.MainModels;
using System.Linq.Expressions;

namespace FineBudget.Services.Specifications
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
