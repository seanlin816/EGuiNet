using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwEInvoice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init_with_twinvoice_3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "accounting_period",
                table: "invoice_books",
                newName: "seller_tax_id");

            migrationBuilder.AddColumn<int>(
                name: "accounting_period_month_first",
                table: "invoice_books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "accounting_period_month_second",
                table: "invoice_books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "accounting_period_tw_year",
                table: "invoice_books",
                type: "integer",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<byte[]>(
                name: "allocated_group_id",
                table: "invoice_books",
                type: "bytea",
                nullable: true);

            migrationBuilder.AddColumn<int>(
                name: "status",
                table: "invoice_books",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "accounting_period_month_first",
                table: "invoice_books");

            migrationBuilder.DropColumn(
                name: "accounting_period_month_second",
                table: "invoice_books");

            migrationBuilder.DropColumn(
                name: "accounting_period_tw_year",
                table: "invoice_books");

            migrationBuilder.DropColumn(
                name: "allocated_group_id",
                table: "invoice_books");

            migrationBuilder.DropColumn(
                name: "status",
                table: "invoice_books");

            migrationBuilder.RenameColumn(
                name: "seller_tax_id",
                table: "invoice_books",
                newName: "accounting_period");
        }
    }
}
