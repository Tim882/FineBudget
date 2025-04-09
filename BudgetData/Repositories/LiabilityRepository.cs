using System;
using Base.Database;
using FineBudget.Models;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Data
{
    public class LiabilityRepository : Repository<Liability, Guid>, IRepository<Liability, Guid>
    {
        public LiabilityRepository(DbContext context) : base(context)
        {
        }
    }
}

