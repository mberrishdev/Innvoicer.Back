using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Innvoicer.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class AddInvoiceKey : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Invoices",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Invoices_Key",
                table: "Invoices",
                column: "Key",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Invoices_Key",
                table: "Invoices");

            migrationBuilder.DropColumn(
                name: "Key",
                table: "Invoices");
        }
    }
}
