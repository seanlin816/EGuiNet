using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TwEInvoice.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class init_with_twinvoice : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "invoice_books",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "bytea", nullable: false),
                    accounting_period = table.Column<string>(type: "text", nullable: false),
                    track = table.Column<string>(type: "text", nullable: false),
                    start_number = table.Column<int>(type: "integer", nullable: false),
                    end_number = table.Column<int>(type: "integer", nullable: false),
                    current_number = table.Column<int>(type: "integer", nullable: false),
                    next_number = table.Column<int>(type: "integer", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_invoice_books", x => x.id);
                });

            migrationBuilder.CreateTable(
                name: "tw_invoice",
                columns: table => new
                {
                    id = table.Column<byte[]>(type: "bytea", nullable: false),
                    buyer_address = table.Column<string>(type: "text", nullable: true),
                    buyer_name = table.Column<string>(type: "text", nullable: false),
                    buyer_tax_id = table.Column<string>(type: "text", nullable: false),
                    invoice_number_serial_number = table.Column<int>(type: "integer", nullable: false),
                    invoice_number_track_id = table.Column<string>(type: "text", nullable: false),
                    seller_address = table.Column<string>(type: "text", nullable: true),
                    seller_name = table.Column<string>(type: "text", nullable: false),
                    seller_tax_id = table.Column<string>(type: "text", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("pk_tw_invoice", x => x.id);
                });
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "invoice_books");

            migrationBuilder.DropTable(
                name: "tw_invoice");
        }
    }
}
