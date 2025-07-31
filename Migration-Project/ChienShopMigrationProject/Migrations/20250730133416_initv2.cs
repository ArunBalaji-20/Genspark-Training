using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ChienShopMigrationProject.Migrations
{
    /// <inheritdoc />
    public partial class initv2 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "PaymentSessionId",
                table: "Orders",
                type: "text",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "PaymentSessionId",
                table: "Orders");
        }
    }
}
