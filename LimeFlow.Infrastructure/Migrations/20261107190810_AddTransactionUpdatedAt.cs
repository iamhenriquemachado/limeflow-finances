using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace LimeFlow.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class AddTransactionUpdatedAt : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "Transactions",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.Sql("""UPDATE "Transactions" SET "UpdatedAt" = "CreatedAt";""");

            migrationBuilder.AlterColumn<DateTime>(
                name: "UpdatedAt",
                table: "Transactions",
                type: "timestamp with time zone",
                nullable: false,
                oldClrType: typeof(DateTime),
                oldType: "timestamp with time zone",
                oldNullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "Transactions");
        }
    }
}
