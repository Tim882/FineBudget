using Base.Database;
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
