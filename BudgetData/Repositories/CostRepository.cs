using System;
using Data.Repository.Interfaces;
using Data.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Models.DbModels.MainModels;

namespace FineBudget.Repositories
{
    public class CostRepository : Repository<Cost, Guid>, IRepository<Cost, Guid>
    {
        public CostRepository(DbContext context) : base(context)
        {
        }
    }
}

