using Base.Database;
using Base.Models;
using FineBudget.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;

namespace FineBudget.Data
{
    public class BudgetUnitOfWork : UnitOfWork<BudgetContext>, IUnitOfWork
    {
        public BudgetUnitOfWork(BudgetContext context) : base(context)
        {
        }
    }
}
