using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Innvoicer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class UpdateInvoiceProperties : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Nights",
                table: "InvoiceItems",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "TotalPrice",
                table: "InvoiceItems",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Nights",
                table: "InvoiceItems");

            migrationBuilder.DropColumn(
                name: "TotalPrice",
                table: "InvoiceItems");
        }
    }
}
