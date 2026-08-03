using FineBudget.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FineBudget.Application.Common.Interfaces
{
    public interface IAppDbContext
    {
        DbSet<Transaction> Transactions { get; }
        DbSet<Category> Categories { get; }
        DbSet<User> Users { get; }
        DbSet<RefreshToken> RefreshTokens { get; }
        Task<int> SaveChangesAsync(CancellationToken cancellationToken = default);
    }
}

