using System;
using Data.Repository.Interfaces;
using Data.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Models.DbModels.MainModels;

namespace FineBudget.Repositories
{
    public class AccountRepository : Repository<Account, Guid>, IRepository<Account, Guid>
    {
        public AccountRepository(DbContext context) : base(context)
        {
        }
    }
}

