using System;
using DbRepository;
using DbRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbModels.MainModels;

namespace FineBudget.Repositories
{
    public class CostRepository : Repository<Cost>, IRepository<Cost>
    {
        public CostRepository(DbContext context) : base(context)
        {
        }
    }
}

