using Base.Database;
using FineBudget.Models;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Data
{
    public class IncomeRepository : Repository<Income, Guid>, IRepository<Income, Guid>
    {
        public IncomeRepository(DbContext context) : base(context)
        {
        }
    }
}

