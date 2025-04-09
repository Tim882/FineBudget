using System;
using Base.Database;
using FineBudget.Models;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Data
{
    public class AccountRepository : Repository<Account, Guid>, IRepository<Account, Guid>
    {
        public AccountRepository(DbContext context) : base(context)
        {
        }
    }
}

