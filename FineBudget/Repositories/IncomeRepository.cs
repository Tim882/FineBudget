using System;
using DbRepository;
using DbRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbModels.MainModels;

namespace FineBudget.Repositories
{
    public class IncomeRepository : Repository<Income>, IRepository<Income>
    {
        public IncomeRepository(DbContext context) : base(context)
        {
        }
    }
}

