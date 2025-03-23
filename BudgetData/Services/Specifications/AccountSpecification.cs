using DbRepository.Specifications;
using Models.DbModels.MainModels;

namespace FineBudget.Services.Specifications
{
    public class AccountSpecification: BaseSpecification<Account>
    {
        public AccountSpecification() : base(null)
        {
            AddInclude(a => a.Costs);
            AddInclude(a => a.Incomes);
        }
    }
}
