using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Trading.Api.Migrations
{
    public partial class removeunique : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tradings_MarketId_CompanyId_Status",
                table: "Tradings");

            migrationBuilder.CreateIndex(
                name: "IX_Tradings_MarketId",
                table: "Tradings",
                column: "MarketId");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Tradings_MarketId",
                table: "Tradings");

            migrationBuilder.CreateIndex(
                name: "IX_Tradings_MarketId_CompanyId_Status",
                table: "Tradings",
                columns: new[] { "MarketId", "CompanyId", "Status" },
                unique: true);
        }
    }
}
