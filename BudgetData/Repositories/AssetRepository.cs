using System;
using Data.Repository.Interfaces;
using Data.Repository.Repositories;
using Microsoft.EntityFrameworkCore;
using Models.DbModels.MainModels;

namespace FineBudget.Repositories
{
    public class AssetRepository : Repository<Asset, Guid>, IRepository<Asset, Guid>
    {
        public AssetRepository(DbContext context) : base(context)
        {
        }
    }
}

