using System;
using DbRepository;
using DbRepository.Interfaces;
using Microsoft.EntityFrameworkCore;
using Models.DbModels.MainModels;

namespace FineBudget.Repositories
{
    public class AssetRepository : Repository<Asset>, IRepository<Asset>
    {
        public AssetRepository(DbContext context) : base(context)
        {
        }
    }
}

