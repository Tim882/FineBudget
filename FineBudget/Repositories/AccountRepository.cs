using System;
using DbRepository;
using DbRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbModels.MainModels;

namespace FineBudget.Repositories
{
    public class AccountRepository : Repository<Account>, IRepository<Account>
    {
        public AccountRepository(DbContext context) : base(context)
        {
        }
    }
}

