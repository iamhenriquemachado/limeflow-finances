using LimeFlow.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace LimeFlow.Infrastructure.Persistence.Configurations;

internal class GoalConfiguration : IEntityTypeConfiguration<Goal>
{
    public void Configure(EntityTypeBuilder<Goal> builder)
    {
        builder.ToTable("Goals");
        builder.HasKey(g => g.Id);

        builder.Property(g => g.Name).HasMaxLength(100).IsRequired();
        builder.Property(g => g.Description).HasMaxLength(500).IsRequired();
        builder.Property(g => g.Icon).HasMaxLength(50).IsRequired();
        builder.Property(g => g.Target).HasPrecision(ColumnTypes.MoneyPrecision, ColumnTypes.MoneyScale);
        builder.Property(g => g.AlreadySaved).HasPrecision(ColumnTypes.MoneyPrecision, ColumnTypes.MoneyScale);
        builder.Property(g => g.MonthlyContribution).HasPrecision(ColumnTypes.MoneyPrecision, ColumnTypes.MoneyScale);
        builder.Property(g => g.TargetDate).IsRequired();
        builder.Property(g => g.CreatedAt).IsRequired();
        builder.Property(g => g.UpdatedAt).IsRequired();

        builder.HasOne<User>()
            .WithMany()
            .HasForeignKey(g => g.UserId)
            .OnDelete(DeleteBehavior.Cascade);
    }
}
