﻿using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
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

            var dateTimeConverter = new ValueConverter<DateTime, DateTime>(
                v => v.ToUniversalTime(),
                v => DateTime.SpecifyKind(v, DateTimeKind.Utc)
            );

            var nullableDateTimeConverter = new ValueConverter<DateTime?, DateTime?>(
                v => v.HasValue ? v.Value.ToUniversalTime() : v,
                v => v.HasValue ? DateTime.SpecifyKind(v.Value, DateTimeKind.Utc) : v
            );

            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (entityType.IsKeyless)
                {
                    continue;
                }

                foreach (var property in entityType.GetProperties())
                {
                    if (property.ClrType == typeof(DateTime))
                    {
                        property.SetValueConverter(dateTimeConverter);
                    }
                    else if (property.ClrType == typeof(DateTime?))
                    {
                        property.SetValueConverter(nullableDateTimeConverter);
                    }
                }
            }
        }
    }
}

