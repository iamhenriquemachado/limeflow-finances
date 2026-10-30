using LimeFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LimeFlow.Infrastructure.Persistence.Configurations;

internal class TransactionConfiguration : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");
        builder.HasKey(t => t.Id);

        builder.Property(t => t.Amount).HasPrecision(ColumnTypes.MoneyPrecision, ColumnTypes.MoneyScale);
        builder.Property(t => t.Description).HasMaxLength(200).IsRequired();
        builder.Property(t => t.Type).IsRequired();
        builder.Property(t => t.TransactionDate).IsRequired();
        builder.Property(t => t.CreatedAt).IsRequired();

        builder.HasIndex(t => new { t.AccountId, t.TransactionDate });
        builder.HasIndex(t => t.CategoryId);

        builder.HasOne<Account>()
            .WithMany()
            .HasForeignKey(t => t.AccountId)
            .OnDelete(DeleteBehavior.Cascade);

        // NoAction (checked at end of statement) lets a user delete cascade through
        // Accounts -> Transactions and Categories without ordering conflicts.
        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(t => t.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
