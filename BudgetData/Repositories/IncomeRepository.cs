using System;
using Data.Repository.Interfaces;
using Data.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Models.DbModels.MainModels;

namespace FineBudget.Repositories
{
    public class IncomeRepository : Repository<Income, Guid>, IRepository<Income, Guid>
    {
        public IncomeRepository(DbContext context) : base(context)
        {
        }
    }
}

