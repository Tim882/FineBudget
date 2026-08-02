using FineBudget.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Transaction> Transactions { get; }
        DbSet<Category> Categories { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

