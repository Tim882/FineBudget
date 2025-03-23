using System;
using Data.Repository.Interfaces;
using Data.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Models.DbModels.MainModels;

namespace FineBudget.Repositories
{
    public class LiabilityRepository : Repository<Liability, Guid>, IRepository<Liability, Guid>
    {
        public LiabilityRepository(DbContext context) : base(context)
        {
        }
    }
}

