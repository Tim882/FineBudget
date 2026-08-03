using System;
using FineBudget.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace FineBudget.Infrastructure.Persistence.Configurations
{
    public class RefreshTokenConfiguration : IEntityTypeConfiguration<RefreshToken>
    {
        public void Configure(EntityTypeBuilder<RefreshToken> builder)
        {
            builder.ToTable("refresh_tokens");
            builder.HasKey(x => x.Id);

            builder.Property(x => x.Token)
                .HasMaxLength(512)
                .IsRequired();

            builder.HasIndex(x => x.Token).IsUnique();

            builder.Property(x => x.ExpiresAt).IsRequired();
            builder.Property(x => x.IsRevoked).IsRequired();
        }
    }
}

