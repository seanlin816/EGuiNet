using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwEInvoice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init_with_twinvoice_2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "invoice_number_string",
                table: "tw_invoice",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "ix_tw_invoice_invoice_number_string",
                table: "tw_invoice",
                column: "invoice_number_string");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "ix_tw_invoice_invoice_number_string",
                table: "tw_invoice");

            migrationBuilder.DropColumn(
                name: "invoice_number_string",
                table: "tw_invoice");
        }
    }
}
