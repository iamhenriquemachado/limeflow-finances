using LimeFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LimeFlow.Infrastructure.Persistence.Configurations;

internal class BudgetConfiguration : IEntityTypeConfiguration<Budget>
{
    public void Configure(EntityTypeBuilder<Budget> builder)
    {
        builder.ToTable("Budgets");
        builder.HasKey(b => b.Id);

        builder.Property(b => b.LimitAmount).HasPrecision(ColumnTypes.MoneyPrecision, ColumnTypes.MoneyScale);
        builder.Property(b => b.AlertThreshold).IsRequired();
        builder.Property(b => b.Period).IsRequired();
        builder.Property(b => b.Notes).HasMaxLength(500);
        builder.Property(b => b.CreatedAt).IsRequired();
        builder.Property(b => b.UpdatedAt).IsRequired();

        builder.HasIndex(b => new { b.CategoryId, b.Period }).IsUnique();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(b => b.UserId)
            .OnDelete(DeleteBehavior.Cascade);

        builder.HasOne<Category>()
            .WithMany()
            .HasForeignKey(b => b.CategoryId)
            .OnDelete(DeleteBehavior.NoAction);
    }
}
