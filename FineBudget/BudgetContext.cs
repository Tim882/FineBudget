using System;
using Microsoft.EntityFrameworkCore;
using Models.DbModels;
using Models.DbModels.BaseModels;
using Models.DbModels.MainModels;

namespace FineBudget
{
	public class BudgetContext: DbContext
	{
		public DbSet<BalanceItem> BalanceItems { get; set; }
		public DbSet<Operation> Operations { get; set; }
		public DbSet<Account> Accounts { get; set; }
		public DbSet<Asset> Assets { get; set; }
		public DbSet<Cost> Costs { get; set; }
		public DbSet<Income> Incomes { get; set; }
		public DbSet<Liability> Liabilities { get; set; }

		public BudgetContext(DbContextOptions<BudgetContext> options) : base(options)
        {
		}

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<BalanceItem>().UseTptMappingStrategy();
            modelBuilder.Entity<Operation>().UseTptMappingStrategy();
        }
    }
}

