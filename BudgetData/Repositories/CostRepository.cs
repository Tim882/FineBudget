using Base.Database;
using FineBudget.Models;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Data
{
    public class CostRepository : Repository<Cost, Guid>, IRepository<Cost, Guid>
    {
        public CostRepository(DbContext context) : base(context)
        {
        }
    }
}

