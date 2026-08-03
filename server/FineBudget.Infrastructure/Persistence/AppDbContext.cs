using System;
using FineBudget.Application.Common.Interfaces;
using FineBudget.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System.Reflection;

namespace FineBudget.Infrastructure.Persistence
{
    public class AppDbContext : DbContext, IAppDbContext
    {
        public DbSet<Transaction> Transactions => Set<Transaction>();
        public DbSet<Category> Categories => Set<Category>();
        public DbSet<User> Users => Set<User>();
        public DbSet<RefreshToken> RefreshTokens => Set<RefreshToken>();

        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
            base.OnModelCreating(modelBuilder);
        }
    }
}

