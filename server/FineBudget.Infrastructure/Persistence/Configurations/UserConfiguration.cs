using System;
using FineBudget.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FineBudget.Infrastructure.Persistence.Configurations
{
    public class UserConfiguration : IEntityTypeConfiguration<User>
    {
        public void Configure(EntityTypeBuilder<User> builder)
        {
            builder.ToTable("users");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Email)
                .HasMaxLength(255)
                .IsRequired();

            builder.HasIndex(x => x.Email).IsUnique();

            builder.Property(x => x.PasswordHash)
                .IsRequired();

            builder.Property(x => x.DisplayName)
                .HasMaxLength(100)
                .IsRequired();

            builder.HasMany(x => x.Transactions)
                .WithOne(t => t.User)
                .HasForeignKey(t => t.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Categories)
                .WithOne(c => c.User)
                .HasForeignKey(c => c.UserId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.RefreshTokens)
                .WithOne(rt => rt.User)
                .HasForeignKey(rt => rt.UserId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }
}

