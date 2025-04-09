using Base.Database;
using FineBudget.Models;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Data
{
    public class AssetRepository : Repository<Asset, Guid>, IRepository<Asset, Guid>
    {
        public AssetRepository(DbContext context) : base(context)
        {
        }
    }
}

