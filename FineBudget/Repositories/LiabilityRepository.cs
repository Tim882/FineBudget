using System;
using DbRepository;
using DbRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbModels.MainModels;

namespace FineBudget.Repositories
{
    public class LiabilityRepository : Repository<Liability>, IRepository<Liability>
    {
        public LiabilityRepository(DbContext context) : base(context)
        {
        }
    }
}

